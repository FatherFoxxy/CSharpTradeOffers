namespace CSharpTradeOffers.Trading
{
    using Newtonsoft.Json;

    [JsonObject(Title = "app_data")]
    public class DescriptionAppData
    {
        [JsonProperty("def_index")]
        public int DefIndex { get; set; }
        [JsonProperty("is_itemset_name")]
        public int IsItemSetName { get; set; }
    }
}