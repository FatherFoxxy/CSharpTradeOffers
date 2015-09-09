namespace CSharpTradeOffers.Trading
{
    using Newtonsoft.Json;

    public class Description
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("app_data")]
        public DescriptionAppData AppData { get; set; }

        [JsonProperty("is_itemset_name")]
        public string IsItemsetName { get; set; }

        [JsonProperty("def_index")]
        public string DefIndex { get; set; }
    }
}