using luminary.trust.json;

namespace luminary.trust;

public class Organization
{
    protected string BaseUrl;
    protected Dictionary<string, List<PublicKeyJson>> SelfPublicKeysByGroup;
    protected Dictionary<string, List<PublicKeyJson>> OpsPublicKeysByGroup;

    public Organization(string _baseUrl, Dictionary<string, List<PublicKeyJson>> _selfPublicKeysByGroup, Dictionary<string, List<PublicKeyJson>> _opsPublicKeysByGroup)
    {
        BaseUrl = _baseUrl;
        SelfPublicKeysByGroup = _selfPublicKeysByGroup;
        OpsPublicKeysByGroup = _opsPublicKeysByGroup;
    }
}