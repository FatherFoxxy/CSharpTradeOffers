using System.Collections.Generic;

namespace CSharpTradeOffers.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Generic officer class.
    /// </summary>
    public class Officer
    {
        /// <summary>
        /// The UInt64 version of a SteamID
        /// </summary>
        [JsonProperty("steamid")]
        public ulong SteamId { get; set; }

        /// <summary>
        /// The permission level the SteamID has.
        /// </summary>
        [JsonProperty("permission_level")]
        public int PermissionLevel { get; set; }

        /// <summary>
        /// Turns the Officer into a KeyValuePair
        /// </summary>
        /// <returns>KeyValuePair of steamid,permissionlevel</returns>
        public KeyValuePair<ulong, int> ToKeyValuePair()
        {
            return new KeyValuePair<ulong, int>(this.SteamId, this.PermissionLevel);
        }
    }
}