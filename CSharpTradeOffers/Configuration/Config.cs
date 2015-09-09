using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CSharpTradeOffers.Configuration
{
    /// <summary>
    /// Generic config file
    /// </summary>
    public class Config : IConfig
    {
        /// <summary>
        /// The meat of the config
        /// </summary>
        public RootConfig Cfg { get; set; } = new RootConfig();

        /// <summary>
        /// Dictionary forms of the lists contained in for easy access/use
        /// </summary>
        public Dictionaries ConfigDictionaries { get; } = new Dictionaries();

        /// <summary>
        /// I forgot why I put this here but it's probably important.
        /// I forgot or it's obvious. TODO: Add better documentation
        /// </summary>
        public string MarketEligibilityJson { get; set; }

        private readonly string path;

        /// <summary>
        /// the Config and the path to use
        /// </summary>
        /// <param name="path">Path of the configuration file</param>
        public Config(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Reloads the configuration file (path). If file is not present, it will generate a new one.
        /// </summary>
        /// <returns>A RootConfig object.</returns>
        public RootConfig Reload()
        {
            if (!File.Exists(this.path))
            {
                File.Create(this.path).Close();

                // BuildMyString.com generated code. Please enjoy your string responsibly.
                var sb = new StringBuilder();

                sb.Append("{\r\n");
                sb.Append("    \"Username\": \"null\",\r\n");
                sb.Append("    \"Password\": \"null\",\r\n");
                sb.Append("    \"SteamMachineAuth\": \"null\",\r\n");
                sb.Append("    \"Api_Key\": \"null\",\r\n");
                sb.Append("    \"Greet_Message\": \"Nada\",\r\n");
                sb.Append("    \"Add_Officers\": true,\r\n");
                sb.Append("    \"Verbose\": true,\r\n");
                sb.Append("    \"Log_Chat\": false,\r\n");
                sb.Append("    \"Log\": true,\r\n");
                sb.Append("    \"Officers\": [{\r\n");
                sb.Append("        \"steamid\": 76561198060315636,\r\n");
                sb.Append("        \"permission_level\":4\r\n");
                sb.Append("    }],\r\n");
                sb.Append("    \"Command_Permissions\": [{\r\n");
                sb.Append("        \"command_name\": \"changename\",\r\n");
                sb.Append("        \"permission_level\": 4\r\n");
                sb.Append("    }],\r\n");
                sb.Append("    \"Inventories\":[440,730],\r\n");
                sb.Append("    \"Banned_Users\": []\r\n");
                sb.Append("}\r\n");

                File.WriteAllText(this.path, sb.ToString());

                this.ConfigDictionaries.OfficersDict.Clear();
                this.ConfigDictionaries.CommandPermissionsDict.Clear();

                foreach (var kvp in this.Cfg.Officers.Select(officer => officer.ToKeyValuePair()))
                {
                    this.ConfigDictionaries.OfficersDict.Add(kvp.Key, kvp.Value);
                }

                foreach (var kvp in this.Cfg.CommandPermissions.Select(permission => permission.ToKeyValuePair()))
                {
                    this.ConfigDictionaries.CommandPermissionsDict.Add(kvp.Key, kvp.Value);
                }
            }

            this.Cfg = JsonConvert.DeserializeObject<RootConfig>(File.ReadAllText(this.path)); // js.Deserialize<RootConfig>(File.ReadAllText(_path));
            return this.Cfg;
        }

        /// <summary>
        /// Writes the changes made to the config.
        /// </summary>
        /// <param name="towrite"></param>
        public void WriteChanges(RootConfig towrite)
        {
            File.WriteAllText(this.path, JsonConvert.SerializeObject(towrite));
        }
    }
}
