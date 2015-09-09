using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace CSharpTradeOffers.Trading
{
    /// <summary>
    /// Handles EconService related tasks such as trade offers.
    /// </summary>
    public class EconServiceHandler
    {
        private const string BaseUrl = "https://api.steampowered.com/IEconService/";

        private readonly string apiKey;

        public EconServiceHandler(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Requests all trade offers.
        /// </summary>
        /// <param name="data">A dictionary containing the URL params found here: https://developer.valvesoftware.com/wiki/Steam_Web_API/IEconService at GetTradeOffers (v1).
        /// ex: [leftarrow]"get_sent_offers","1"[rightarrow]. Please note, [leftarrow] and [rightarrow] correspond to the keys on the keyboard.</param>
        /// <returns></returns>
        public TradeOffersList GetTradeOffers(Dictionary<string, string> data)
        {
            data.Add("key", this.apiKey);
            data.Add("format", "json");
            return
                JsonConvert.DeserializeObject<TradeOffers>(
                    WebUtility.UrlDecode(Web.Fetch(BaseUrl + "GetTradeOffers/v1/", "GET", data))).Response;
        }

        /// <summary>
        /// Requests a single trade offer based on the tradeofferid.
        /// </summary>
        /// <param name="tradeofferid">The tradeofferid to request information on.</param>
        /// <param name="language">The language to use. Default: english</param>
        /// <returns>A CEConTradeOffer object.</returns>
        public CEconTradeOffer GetTradeOffer(ulong tradeofferid, string language = "english")
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"tradeofferid", tradeofferid.ToString()},
                {"language", language},
                {"format", "json"}
            };
            return
                JsonConvert.DeserializeObject<CEconTradeOffer>(
                    WebUtility.UrlDecode(Web.Fetch(BaseUrl + "GetTradeOffer/v1/", "GET", data)));
        }

        /// <summary>
        /// Declines a trade offer that was sent to you.
        /// </summary>
        /// <param name="tradeofferid">The ID of the offer to decline.</param>
        /// <returns></returns>
        public string DeclineTradeOffer(ulong tradeofferid)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"tradeofferid", tradeofferid.ToString()},
                {"format", "json"}
            };
            return Web.Fetch(BaseUrl + "DeclineTradeOffer/v1/", "POST", data);
        }

        /// <summary>
        /// Cancels a trade offer based on the specified tradeofferid.
        /// </summary>
        /// <param name="tradeofferid">The ID of the offer to cancel.</param>
        /// <returns></returns>
        public string CancelTradeOffer(ulong tradeofferid)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"tradeofferid", tradeofferid.ToString()},
                {"format", "json"}
            };
            return WebUtility.UrlDecode(Web.Fetch(BaseUrl + "CancelTradeOffer/v1/", "POST", data));
        }

        /// <summary>
        /// Accepts a specified trade offer.
        /// </summary>
        /// <param name="tradeId">A TradeId object containing the id of the trade to accept.</param>
        /// <param name="container">Auth Cookies MUST be passed here, the function will fail if not.</param>
        /// <param name="partnerId">The AccountId of the person to trade with.</param>
        /// <param name="serverid">Almost always 1, not quite sure what other numbers do.</param>
        /// <returns>The TradeId of the offer that was accepted.</returns>
        public Trade AcceptTradeOffer(Trade tradeId, CookieContainer container, uint partnerId, string serverid)
        {
            container.Add(new Cookie("bCompletedTradeOfferTutorial", "true") { Domain = "steamcommunity.com" });
            var data = new Dictionary<string, string>
            {
                {"sessionid", Web.SessionId},
                {"serverid", serverid},
                {"tradeofferid", tradeId.TradeId.ToString()},
                {"partner", SteamIdOperations.ConvertAccountIdToUlong(partnerId).ToString()},
                {"captcha", string.Empty}
            };
            return
                JsonConvert.DeserializeObject<Trade>(
                    WebUtility.UrlDecode(Web.Fetch(
                        $"https://steamcommunity.com/tradeoffer/{tradeId.TradeId}/accept", "POST",
                        data, container, false, "https://steamcommunity.com/tradeoffer/" + tradeId.TradeId + "/")));
        }

        /// <summary>
        /// Accepts a specified trade offer.
        /// </summary>
        /// <param name="tradeId">A ulong representing the trade to accept.</param>
        /// <param name="container">Auth Cookies MUST be passed here, the function will fail if not.</param>
        /// <param name="partnerId">The AccountId of the person to trade with.</param>
        /// <param name="serverid">Almost always 1, not quite sure what other numbers do.</param>
        /// <returns>The TradeId of the offer that was accepted.</returns>
        public Trade AcceptTradeOffer(ulong tradeId, CookieContainer container, uint partnerId, string serverid)
        {
            container.Add(new Cookie("bCompletedTradeOfferTutorial", "true") { Domain = "steamcommunity.com" });
            var data = new Dictionary<string, string>
            {
                {"sessionid", Web.SessionId},
                {"serverid", serverid},
                {"tradeofferid", tradeId.ToString()},
                {"partner", SteamIdOperations.ConvertAccountIdToUlong(partnerId).ToString()},
                {"captcha", string.Empty}
            };
            return
                JsonConvert.DeserializeObject<Trade>(
                    WebUtility.UrlDecode(Web.Fetch(
                        $"https://steamcommunity.com/tradeoffer/{tradeId}/accept", "POST",
                        data, container, false, "https://steamcommunity.com/tradeoffer/" + tradeId + "/")));
        }

        /// <summary>
        /// Sends a trade offer to the specified recipient. 
        /// </summary>
        /// <param name="partnerSid">The SteamId64 (ulong) of the person to send the offer to.</param>
        /// <param name="container">Auth Cookies MUST be passed here, the function will fail if not.</param>
        /// <param name="tradeoffermessage">An optional message to be sent with the offer. Can be null.</param>
        /// <param name="serverid">Almost always 1, not quite sure what other numbers do.</param>
        /// <param name="offer">A TradeOffer object containing the trade parameters.</param>
        /// <returns>A SendOfferResponse object.</returns>
        public SendOfferResponse SendTradeOffer(
            ulong partnerSid, CookieContainer container, string tradeoffermessage,
            string serverid, TradeOffer offer)
        {
            container.Add(new Cookie("bCompletedTradeOfferTutorial", "true") { Domain = "steamcommunity.com" });

            var data = new Dictionary<string, string>
            {
                {"sessionid", Web.SessionId},
                {"serverid", serverid},
                {"partner", partnerSid.ToString()},
                {"tradeoffermessage", tradeoffermessage},
                {"json_tradeoffer", JsonConvert.SerializeObject(offer)},
                {"captcha", string.Empty},
                {"trade_offer_create_params", "{}"}
            };
            uint accountId =
                SteamIdOperations.ConvertSteamIdToAccountId(SteamIdOperations.ConvertUlongToSteamId(partnerSid));

            return JsonConvert.DeserializeObject<SendOfferResponse>(
                Web.Fetch(
                    "https://steamcommunity.com/tradeoffer/new/send", "POST", data, container, false,
                    "https://steamcommunity.com/tradeoffer/new/?partner=" + accountId));
        }

        /// <summary>
        /// Modifies an existing trade offer.
        /// </summary>
        /// <param name="partnerSid">The SteamId64 (ulong) of the person whose offer will be modified.</param>
        /// <param name="container">Auth Cookies MUST be passed here, the function will fail if not.</param>
        /// <param name="tradeoffermessage">An otpional message to be sent with the trade offer. Can be null.</param>
        /// <param name="serverid">Almost always 1, not quite sure what other numbers do.</param>
        /// <param name="tradeofferidCountered">The TradeId of the offer to counter or modify.</param>
        /// <param name="offer">A TradeOffer object containing the trade parameters. </param>
        /// <returns>A SendOfferResponse object.</returns>
        public SendOfferResponse ModifyTradeOffer(
            ulong partnerSid, CookieContainer container, string tradeoffermessage,
            string serverid, uint tradeofferidCountered, TradeOffer offer)
        {
            container.Add(new Cookie("bCompletedTradeOfferTutorial", "true") { Domain = "steamcommunity.com" });

            var data = new Dictionary<string, string>
            {
                {"sessionid", Web.SessionId},
                {"serverid", serverid},
                {"partner", partnerSid.ToString()},
                {"tradeoffermessage", tradeoffermessage},
                {"json_tradeoffer", JsonConvert.SerializeObject(offer)},
                {"captcha", string.Empty},
                {"trade_offer_create_params", "{}"},
                {"tradeofferid_countered", tradeofferidCountered.ToString()}
            };
            return JsonConvert.DeserializeObject<SendOfferResponse>(
                Web.Fetch(
                    "https://steamcommunity.com/tradeoffer/new/send", "POST", data, container, false,
                    "https://steamcommunity.com/tradeoffer/" + tradeofferidCountered + "/"));
        }

        /// <summary>
        /// Requestss TradeOffer statistics. ie: historical_received_count
        /// </summary>
        /// <param name="timeLastVisit">Unix time for historical cutoff.</param>
        /// <returns></returns>
        public GetTradeOffersSummaryResponse GetTradeOffersSummary(uint timeLastVisit)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"time_last_visit", timeLastVisit.ToString()}
            };
            return
                JsonConvert.DeserializeObject<GetTradeOffersSummaryBaseResponse>(Web.Fetch(BaseUrl + "GetTradeOffersSummary/v1/", "GET", data)).Response;
        }
    }
}