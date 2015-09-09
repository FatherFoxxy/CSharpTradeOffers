namespace CSharpTradeOffers.Trading
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject(Title = "Filter_Data")]
    public class FilterData
    {
        [JsonProperty("element_ids")]
        public List<Elements> ElementIds { get; } = new List<Elements>();
    }
}