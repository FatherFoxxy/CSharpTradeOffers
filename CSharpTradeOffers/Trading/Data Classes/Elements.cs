namespace CSharpTradeOffers.Trading
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject(Title = "Element_IDs")]
    public class Elements
    {
        [JsonProperty("ids")]
        public List<string> Ids { get; } = new List<string>();
    }
}