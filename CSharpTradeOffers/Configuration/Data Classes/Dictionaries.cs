using System.Collections.Generic;

namespace CSharpTradeOffers.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class containing Dictionary versions of Offer/CommandPermission
    /// </summary>
    public class Dictionaries
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Officers_Dict")]
        public Dictionary<ulong, int> OfficersDict { get; } = new Dictionary<ulong, int>();

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Command_Permissions_Dict")]
        public Dictionary<string, int> CommandPermissionsDict { get; } = new Dictionary<string, int>();

        /// <summary>
        /// Clear officers dictionary. This should not be called directly.
        /// </summary>
        public void ClearOfficer()
        {
            this.OfficersDict.Clear();
        }

        /// <summary>
        /// Clear permissions dictionary. This shold not be called directly.
        /// </summary>
        public void ClearPerms()
        {
            this.CommandPermissionsDict.Clear();
        }
    }
}