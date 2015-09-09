﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CSharpTradeOffers.Trading
{
    /// <summary>
    /// Handles Steam Economy related tasks, like retrieving class info
    /// </summary>
    public class SteamEconomyHandler
    {
        private const string BaseUrl = "https://api.steampowered.com/ISteamEconomy/";
        private readonly string _apiKey;

        public SteamEconomyHandler(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Gets the asset class info of an item, must provide ClassID/InstanceID in IDs
        /// </summary>
        /// <param name="apiKey">Your Steam API key</param>
        /// <param name="appid">Uint32 number that represents the game to retrieve item data from.</param>
        /// <param name="Ids">Dictionary MUST contain ClassID/InstanceID of item.</param>
        /// <returns></returns>
        public AssetClassInfo GetAssetClassInfo(uint appid, Dictionary<string, string> ids)
        {
            const string url = BaseUrl + "GetAssetClassInfo/v0001/";
            var data = new Dictionary<string, string>
            {
                {"key", _apiKey},
                {"appid", appid.ToString()},
                {"class_count", ids.Count.ToString()}
            };
            int currentClass = 0;
            // make only request per appid at a time
            foreach (var key in ids) 
            {
                data.Add("classid" + currentClass, key.Key);
                data.Add("instanceid" + currentClass, key.Value);
                currentClass++;
            }

            dynamic dynamicinfo = JsonConvert.DeserializeObject<dynamic>(Web.Fetch(url, "GET", data, null, false)).result;

            Dictionary<string, dynamic> desrDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(dynamicinfo.ToString());

            desrDictionary.Remove("success");

            return JsonConvert.DeserializeObject<AssetClassInfo>(desrDictionary.Values.First().ToString());
        }

        /// <summary>
        /// I think this retrieves coupons or something. Although it may do what it says.
        /// </summary>
        /// <param name="appId">AppId to find asset prices for.</param>
        /// <param name="currency">Three letter string representing a currency to filter for, if left blank all currencies are returned. EX: USD or EUR, etc.</param>
        /// <param name="language">The language to retrieve the results in.</param>
        /// <returns>A GetAssetPricesResponse object.</returns>
        public GetAssetPricesResponse GetAssetPrices(uint appId, string currency = "", string language = "en")
        {
            const string url = BaseUrl + "GetAssetPrices/v1/";
            var data = new Dictionary<string, string>
            {
                {"key", _apiKey},
                {"appid", appId.ToString()},
                {"currency", currency},
                {"language", language}
            };
            return
                JsonConvert.DeserializeObject<GetAssetPricesBaseResponse>(Web.Fetch(url, "GET", data, null, false))
                    .Result;
        }
    }
}