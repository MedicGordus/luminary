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
            if (
                _update is
                {
                    Url: not null,
                    PublicKey: not null,
                    Nuance: not null,
                    DatetimeStamp: not null,
                    SignatureBase64: not null
                }
            )
            {
                var messageBytes = _update.GetSignableData()?.ToUtf8Bytes();

                if (messageBytes != null)
                {
                    string lowercaseUrl = _update.Url.ToLower();

                    // make sure this org is trusted
                    Organization? trustedOrganizationMatch = null;
                    //
                    // note that we cannot access TrustedOrganizations without an asynclock:
                    using (await AuthUpdateLock.LockAsync().ConfigureAwait(false))
                    {
                        trustedOrganizationMatch = TrustedOrganizations.FirstOrDefault(_item => _item.BaseUrl == lowercaseUrl);
                    }
                    if (trustedOrganizationMatch == null)
                    {
                        return;
                    }
                    if (!trustedOrganizationMatch.SelfContainsPublicKey(_update.PublicKey))
                    {
                        return;
                    }

                    // make sure the signature is good
                    if (!Cryptography.VerifySignature(_update.PublicKey, messageBytes, _update.SignatureBase64))
                    {
                        return;
                    }

                    // store the public key, time and nuance to prevent receiving the same payload more than once
                    //
                    //  This handles a special case where a malicious actor repeatedly sends a valid payload.
                    //
                    todo();

                    // collect the new information
                    ScrubbableResult<Organization> org = await BuildOrganizationFromUrlAsync(lowercaseUrl).ConfigureAwait(false);
                    if (!org.Scrub && org.ReturnValue != null)
                    {
                        using (await AuthUpdateLock.LockAsync().ConfigureAwait(false))
                        {
                            TrustedOrganizations.RemoveAll(
                                _item => _item.BaseUrl == _update.Url
                            );

                            TrustedOrganizations.Add(org.ReturnValue);
                        }
                    }
                }
            }
        }
        catch { }
    }

    public static async Task<ScrubbableResult<Gleam>> CreateAsync(List<string> _trustedOrganizationUrls)
    {
        ScrubbableResult<Gleam> output = new();

        try
        {
            List<Organization> trustedOrganizations = [];

            foreach (string deltaUrl in _trustedOrganizationUrls)
            {
                ScrubbableResult<Organization> org = await BuildOrganizationFromUrlAsync(deltaUrl).ConfigureAwait(false);

                if (org.Scrub)
                {
                    output.AddResults(org.GetResults());
                    break;
                }

                if (org.ReturnValue != null)
                {
                    trustedOrganizations.Add(org.ReturnValue);
                }
            }

            output.ReturnValue = new Gleam(trustedOrganizations);
        }
        catch (Exception e)
        {
            output.ActivateScrub(e);
        }

        return output;
    }


    /// <summary>
    /// Retrieves organization information via http/https to build an Organization object.
    /// </summary>
    /// <param name="_url">Base url for the trusted organization.</param>
    /// <returns></returns>
    public static async Task<ScrubbableResult<Organization>> BuildOrganizationFromUrlAsync(string _url)
    {
        ScrubbableResult<Organization> output = new();

        try
        {
            //// get self creds for the organization
            //
            Scrubbable<CredsSelfJson?> selfCreds = await JsonRetriever.GetJsonAsync<CredsSelfJson>(
                string.Format(
                    "{0}{1}",
                    _url,
                    URL_CREDS_SELF
                )
            ).ConfigureAwait(false);
            //
            if (selfCreds.Scrub)
            {
                output.AddResult(
                    $"Attempt to retrieve self creds for '{_url}' failed, the message was: '{selfCreds.GetException().Message}'."
                );
                return output;
            }
            //
            ////

            //// get self creds for the organization
            //
            Scrubbable<CredsOpsJson?> opsCreds = await JsonRetriever.GetJsonAsync<CredsOpsJson>($"{_url}{URL_CREDS_OPS}").ConfigureAwait(false);
            //
            if (opsCreds.Scrub)
            {
                output.AddResult(
                    $"Attempt to retrieve ops creds for '{_url}' failed, the message was: '{opsCreds.GetException().Message}'."
                );
                return output;
            }
            //
            ////


            // build organization
            output.ReturnValue = new Organization(
                _url.ToLower(),
                selfCreds.ReturnValue?.PublicKeysByGroup ?? [],
                opsCreds.ReturnValue?.PublicKeysByGroup ?? []
            );
        }
        catch (Exception e)
        {
            output.ActivateScrub(e);
        }

        return output;
    }

    public static async Task<ScrubbableResult<ApiSecretJson>> RetrieveSecretForOrganizationAsync(string _organizationUrl, string _organizationGroup)
    {
        todo();
        throw new NotImplementedException();
    }
}