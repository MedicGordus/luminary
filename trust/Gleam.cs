using luminary.trust.json;
using luminary.util;
using static luminary.util.Bytes;

namespace luminary.trust;

/// <summary>
/// Handles all trust operations.
/// </summary>
public class Gleam
{

#region "url constants"
    public const string URL_API = "/api";

    //// creds paths
    //
    public const string URL_CREDS = URL_API + "/creds";
    //
    /// <summary>
    /// List of public keys by group which represent this organization.
    /// </summary>
    public const string URL_CREDS_SELF = URL_CREDS + "/self";
    //
    /// <summary>
    /// List of public keys by group which are trusted operators of this organization.
    /// </summary>
    public const string URL_CREDS_OPS = URL_CREDS + "/ops";
    //
    /// <summary>
    /// List of revoked public keys by group.
    /// </summary>
    public const string URL_CREDS_DELETED = URL_CREDS + "/deleted";
    //
    ////

    //// auth paths
    //
    public const string URL_AUTH = URL_API + "/auth";
    //
    /// <summary>
    /// This is called by remote organizations to get new or updates secrets to allow for data collection from the data endpoints.
    /// </summary>
    public const string URL_AUTH_GET_SECRET = URL_AUTH + "/getsecret";
    //
    /// <summary>
    /// This is called by remote organizations informing of new and/or revoked public keys.
    /// </summary>
    public const string URL_AUTH_UPDATE = URL_AUTH + "/update";
    //
    ////
#endregion


    /// <summary>
    /// Organizations that are trusted.
    /// </summary>
    protected readonly List<Organization> TrustedOrganizations;

    protected readonly AsyncLock AuthUpdateLock = AsyncLock.Create();

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="_trustedOrganizations">See TrustedOrganizations.</param>
    protected Gleam(List<Organization> _trustedOrganizations)
    {
        TrustedOrganizations = _trustedOrganizations;
    }

    public async Task ProcessAuthUpdateAsync(AuthUpdateJson _update)
    {
        try
        {
            if(_update != null && _update.Url != null && _update.PublicKey != null && _update.SignatureBase64 != null)
            {
                var _message = _update.GetSignableData();

                if(_message != null)
                {
                    string _lowercaseUrl = _update.Url.ToLower();

                    // make sure this org is trusted
                    Organization? _trustedOrganizationMatch = null;
                    //
                    // note that we cannot access TrustedOrganizations without an asynclock:
                    using(await AuthUpdateLock.LockAsync())
                    {
                        _trustedOrganizationMatch = TrustedOrganizations.FirstOrDefault(_item => _item.BaseUrl == _lowercaseUrl);
                    }
                    if(_trustedOrganizationMatch == null)
                    {
                        return;
                    }
                    if(!_trustedOrganizationMatch.SelfContainsPublicKey(_update.PublicKey))
                    {
                        return;
                    }

                    // make sure the signature is good
                    if(!Cryptography.VerifySignature(_update.PublicKey, _message.ToUtf8Bytes(), _update.SignatureBase64))
                    {
                        return;
                    }

                    // collect the new information
                    ScrubbableResult<Organization> _org = await BuildOrganizationFromUrlAsync(_lowercaseUrl);
                    if(!_org.Scrub && _org.ReturnValue != null)
                    {
                        using(await AuthUpdateLock.LockAsync())
                        {
                            TrustedOrganizations.RemoveAll(
                                _item => _item.BaseUrl == _update.Url
                            );

                            TrustedOrganizations.Add(_org.ReturnValue);
                        }
                    }
                }
            }
        }
        catch {}
    }

    public static async Task<ScrubbableResult<Gleam>> CreateAsync(List<string> _trustedOrganizationUrls)
    {
        ScrubbableResult<Gleam> _output = new();

        try
        {
            List<Organization> _trustedOrganizations = [];

            foreach(string _deltaUrl in _trustedOrganizationUrls)
            {
                ScrubbableResult<Organization> _org = await BuildOrganizationFromUrlAsync(_deltaUrl);

                if(_org.Scrub)
                {
                    _output.AddResults(_org.GetResults());
                    break;
                }

                if(_org.ReturnValue != null)
                {
                    _trustedOrganizations.Add(_org.ReturnValue);
                }
            }

            _output.ReturnValue = new Gleam(_trustedOrganizations);
        }
        catch (Exception _e)
        {
            _output.ActivateScrub(_e);
        }

        return _output;
    }


    /// <summary>
    /// Retrieves organization information via http/https to build an Organization object.
    /// </summary>
    /// <param name="_url">Base url for the trusted organization.</param>
    /// <returns></returns>
    public static async Task<ScrubbableResult<Organization>> BuildOrganizationFromUrlAsync(string _url)
    {
        ScrubbableResult<Organization> _output = new();

        try
        {
            //// get self creds for the organization
            //
            Scrubbable<CredsSelfJson?> _selfCreds = await JsonRetriever.GetJsonAsync<CredsSelfJson>(
                string.Format(
                    "{0}{1}",
                    _url,
                    URL_CREDS_SELF
                )
            );
            //
            if(_selfCreds.Scrub)
            {
                _output.AddResult(
                    string.Format(
                        "Attempt to retrieve self creds for '{0}' failed, the message was: '{1}'.",
                        _url,
                        _selfCreds.GetException().Message
                    )
                );
                return _output;
            }
            //
            ////

            //// get self creds for the organization
            //
            Scrubbable<CredsOpsJson?> _opsCreds = await JsonRetriever.GetJsonAsync<CredsOpsJson>(
                string.Format(
                    "{0}{1}",
                    _url,
                    URL_CREDS_OPS
                )
            );
            //
            if(_opsCreds.Scrub)
            {
                _output.AddResult(
                    string.Format(
                        "Attempt to retrieve ops creds for '{0}' failed, the message was: '{1}'.",
                        _url,
                        _opsCreds.GetException().Message
                    )
                );
                return _output;
            }
            //
            ////


            // build organization
            _output.ReturnValue = new Organization(
                _url.ToLower(),
                _selfCreds.ReturnValue?.PublicKeysByGroup ?? [],
                _opsCreds.ReturnValue?.PublicKeysByGroup  ?? []
            );
        }
        catch (Exception _e)
        {
            _output.ActivateScrub(_e);
        }

        return _output;
    }

    public static async Task<ScrubbableResult<ApiSecretJson>> RetrieveSecretForOrganizationAsync(string _organizationUrl, string _organizationGroup)
    {}
}