namespace CSharpTradeOffers.Trading
{
    using Newtonsoft.Json;

    [JsonObject(Title = "class")]
    public class Class
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}