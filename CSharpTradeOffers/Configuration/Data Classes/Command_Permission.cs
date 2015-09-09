using System.Collections.Generic;

namespace CSharpTradeOffers.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Generic command permission class.
    /// </summary>
    [JsonObject(Title = "Command_Permission")]
    public class CommandPermission
    {
        /// <summary>
        /// The exact name of the command.
        /// </summary>
        [JsonProperty("command_name")]
        public string CommandName { get; set; }

        /// <summary>
        /// An integer representing a permission level.
        /// </summary>
        [JsonProperty("permission_level")]
        public int PermissionLevel { get; set; }

        /// <summary>
        /// Turns the Command_Permission into a KeyValuePair
        /// </summary>
        /// <returns>KeyValuePair</returns>
        public KeyValuePair<string, int> ToKeyValuePair()
        {
            return new KeyValuePair<string, int>(this.CommandName, this.PermissionLevel);
        }
    }
}