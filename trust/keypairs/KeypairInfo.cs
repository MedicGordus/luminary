using luminary.util;


using System.Text.Json;
using System.Text.Json.Serialization;

namespace luminary.trust;

/// <summary>
/// This holds reference to info about a public keypair used by Gleam.
/// 
/// This is sharable.
/// </summary>
public class KeypairInfo 
{
    public struct KeyPairType
    {
        /// <summary>
        /// Used for https connections in place of a root CA.
        /// 
        /// All organizations trade public keys outside of this mechanism once.
        /// 
        /// Renewals are done automatically within the system, as multiple can
        ///     be valid at any given time with overlapping or no expiration.
        /// 
        /// Replacement can be done in case of revocation outside of this mechanism.
        /// 
        /// Different listeners may use different root keys.
        /// </summary>
        public const string ROOT_GLEAM = "root";

        /// <summary>
        /// Signed by the Root Gleam key, used for https connections. This is
        ///     trusted based on the root signer, so it passes the chain of
        ///     itself and the root key every time.
        /// 
        /// Clients do not store these, only the https listener side (privately).
        /// </summary>
        public const string LEAF_GLEAM = "leaf";

        /// <summary>
        /// A keypair assigned to a group (any given string).
        /// 
        /// This grants authorization based on the systems that recognize these
        ///     groups. Self and Operative group authentication keypairs are
        ///     identical - they are separated purely so others know which is
        ///     the entity and which are grants to external parties (although
        ///     there is no enforcement of this sorting).
        /// </summary>
        public const string GROUP_AUTHENTICATION = "group";
    }

    /// <summary>
    /// Organization class related to keypairs.
    /// </summary>
    public class Organization
    {
        
        public const string FIELD_LABEL = "label";
        /// <summary>
        /// Arbitrary label applied to this organization.
        /// </summary>
        [JsonPropertyName(FIELD_LABEL)]
        public string? Label { get; set; }

        public const string FIELD_URL = "url";
        /// <summary>
        /// Holds reference to the url path which Gleam paths can be appended to for access.
        /// </summary>
        [JsonPropertyName(FIELD_URL)]
        public string? Url { get; set; }

        public const string FIELD_GROUPS = "groups";
        /// <summary>
        /// Holds reference to all groups related to this organization.
        /// 
        /// Note that any overlap with another organization indicates the group belongs to both organizations.
        /// </summary>
        [JsonPropertyName(FIELD_GROUPS)]
        public List<string>? Groups { get; set; }

        /// <summary>
        /// This allows for custom lookups to be applied to a group.
        /// 
        /// The first level is the "system" level, the second is the group level.
        /// </summary>
        /// <remarks>
        /// Say an integration needs an internal record primary key based on the public key it received.
        /// 
        /// It could look up the group here, then from the group, lookup the internal identifier by the system.
        /// </remarks>
        public Dictionary<string,Dictionary<string, string>>? InternalIdentifierGroupLookups { get; set; }

        /// <summary>
        /// This allows for custom lookups to be applied to an organization.
        /// 
        /// The lookup is the "system" level, pass the system in for this organization to retrieve the internal id.
        /// </summary>
        /// <remarks>
        /// Say an integration needs an internal record primary key based on the public key it received.
        /// 
        /// It could look up the organization here by using lookup of the internal identifier by the system.
        /// </remarks>
        public Dictionary<string, string>? InternalIdentifierOrganizationLookups { get; set; }

    }
}