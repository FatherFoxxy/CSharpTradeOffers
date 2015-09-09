namespace CSharpTradeOffers.Trading
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Asset
    {
        [JsonProperty("prices")]
        public Dictionary<string, string> Prices { get; set; }
        [JsonProperty("original_prices")]
        public Dictionary<string, string> OriginalPrices { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("class")]
        public Class[] Class { get; set; }
        [JsonProperty("classid")]
        public string ClassId { get; set; }
    }
}