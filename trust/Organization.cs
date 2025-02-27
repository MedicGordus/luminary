using luminary.util;

namespace luminary.trust;

public class Organization
{
    public readonly string BaseUrl;
    protected Dictionary<string, List<PublicKeyJson>> SelfPublicKeysByGroup;
    protected Dictionary<string, List<PublicKeyJson>> OpsPublicKeysByGroup;

    public Organization(string _baseUrl, Dictionary<string, List<PublicKeyJson>> _selfPublicKeysByGroup, Dictionary<string, List<PublicKeyJson>> _opsPublicKeysByGroup)
    {
        BaseUrl = _baseUrl;
        SelfPublicKeysByGroup = _selfPublicKeysByGroup;
        OpsPublicKeysByGroup = _opsPublicKeysByGroup;
    }

    public bool SelfContainsPublicKey(PublicKeyJson _publicKeyToCheck)
    {
        return SelfPublicKeysByGroup.Any(_item =>
            _item.Value.Any(_selfPublicKey =>
                    _selfPublicKey.KeyType == _publicKeyToCheck.KeyType
                &&
                    _selfPublicKey.PublicKeyBase64 == _publicKeyToCheck.PublicKeyBase64
            )
        );
    }
}