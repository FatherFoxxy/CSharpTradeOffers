namespace CSharpTradeOffers.Trading
{
    using Newtonsoft.Json;

    [JsonObject(Title = "RootObject")]
    public class GetAssetPricesBaseResponse
    {
        [JsonProperty("result")]
        public GetAssetPricesResponse Result { get; set; }
    }
}