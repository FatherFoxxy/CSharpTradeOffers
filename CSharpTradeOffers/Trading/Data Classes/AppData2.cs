namespace CSharpTradeOffers.Trading
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class AppData2
    {
        [JsonProperty("def_index")]
        public string DefIndex { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("slot")]
        public string Slot { get; set; }

        [JsonProperty("set_bundle_def_index")]
        public string SetBundleDefIndex { get; set; }

        [JsonProperty("containing_bundles")]
        public List<string> ContainingBundles { get; } = new List<string>();

        [JsonProperty("filter_data")]
        public List<FilterData> Data { get; } = new List<FilterData>();

        [JsonProperty("player_class_ids")]
        public List<string> PlayerClassIds { get; } = new List<string>();
    }
}