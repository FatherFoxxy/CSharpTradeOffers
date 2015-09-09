using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSharpTradeOffers.Trading
{
    public class GetAssetPricesResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("assets")]
        public List<Asset> Assets { get; set; }
    }
}
