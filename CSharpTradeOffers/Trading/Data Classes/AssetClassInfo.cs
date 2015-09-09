using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSharpTradeOffers.Trading
{
    [JsonObject(Title = "RootObject")]
    public class AssetClassInfo
    {
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("icon_url_large")]
        public string IconUrlLarge { get; set; }

        [JsonProperty("icon_drag_url")]
        public string IconDragUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("market_hash_name")]
        public string MarketHashName { get; set; }

        [JsonProperty("market_name")]
        public string MarketName { get; set; }

        [JsonProperty("name_color")]
        public string NameColor { get; set; }

        [JsonProperty("background_color")]
        public string BackgroundColor { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tradable")]
        public string Tradable { get; set; }

        [JsonProperty("marketable")]
        public string Marketable { get; set; }

        [JsonProperty("commodity")]
        public string Commodity { get; set; }

        [JsonProperty("market_tradable_restriction")]
        public string MarketTradableRestriction { get; set; }

        [JsonProperty("fraudwarnings")]
        public Dictionary<string, dynamic> FraudWarnings { get; set; }

        [JsonProperty("descriptions")]
        public Dictionary<string, Description> Descriptions { get; set; }

        [JsonProperty("owner_descriptions")]
        public string OwnerDescriptions { get; set; }

        [JsonProperty("actions")]
        public Dictionary<string, Action> Actions { get; set; }

        [JsonProperty("market_actions")]
        public Dictionary<string, dynamic> MarketActions { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, Tag> Tags { get; set; }

        [JsonProperty("classid")]
        public string ClassId { get; set; }

        [JsonProperty("instanceid")]
        public string InstanceId { get; set; }
    }
}