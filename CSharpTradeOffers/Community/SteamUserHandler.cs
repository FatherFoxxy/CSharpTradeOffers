﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CSharpTradeOffers.Community
{
    /// <summary>
    /// Handles steam user related tasks.
    /// </summary>
    public class SteamUserHandler
    {
        private const string BaseUrl = "http://api.steampowered.com/ISteamUser/";
        private readonly string apiKey;

        public SteamUserHandler(string apiKey)
        {
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Retrieves the friends list of the specified steamid64. 
        /// The profile must be set to public or the owner of the api key must be friends with them.
        /// The profile cannot be private or the method will fail and it will return null.
        /// </summary>
        /// <param name="steamId">SteamId64 to retrieve the friends list from.</param>
        /// <param name="relationship">All/Friend, there are others but I do not know what.</param>
        /// <returns>Null upon failure, otherwise a list of Friend objects.</returns>
        public List<Friend> GetFriendList(ulong steamId, string relationship = "" )
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"steamid", steamId.ToString()},
                {"relationship", relationship}
            };
            return
                JsonConvert.DeserializeObject<GetFriendListResult>(Web.Fetch(BaseUrl + "GetFriendList/v1/", "GET", data, null, false))
                    .Friendslist.Friends;
        }

        /// <summary>
        /// Gets the bans of the specified SteamId64s
        /// </summary>
        /// <param name="playersBansToRequest">A List of steamid64s to retrieve ban information about.</param>
        /// <returns></returns>
        public List<PlayerBans> GetPlayerBans(List<ulong> playersBansToRequest)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"steamids", CommaDelimit(playersBansToRequest)}
            };
            return
                JsonConvert.DeserializeObject<GetPlayerBansResult>(Web.Fetch(BaseUrl + "GetPlayerBans/v1/", "GET", data, null, false)).PlayerBans;
        }

        /// <summary>
        /// Requests a list of player summaries of the players in the list.
        /// </summary>
        /// <param name="playerSummariesToRequest">A list of SteamIds to request their summaries.</param>
        /// <returns>A list of PlayerSummary objects.</returns>
        public List<PlayerSummary> GetPlayerSummariesV2(List<ulong> playerSummariesToRequest)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"steamids", CommaDelimit(playerSummariesToRequest)}
            };
            return
                JsonConvert.DeserializeObject<GetPlayerSummariesV2Result>(Web.Fetch(BaseUrl + "GetPlayerSummaries/v2/", "GET", data, null, false))
                    .Response.PlayersSummaries;
        }

        /// <summary>
        /// Requests the GroupIds of the groups of the specified player.
        /// </summary>
        /// <param name="steamId">SteamId64 of the player.</param>
        /// <returns>A GetUserGroupListResult object that contains a list of group ids.</returns>
        public GetUserGroupListResult GetUserGroupList(ulong steamId)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"steamid", steamId.ToString()}
            };
            return
                JsonConvert.DeserializeObject<GetUserGroupListBaseResult>(Web.Fetch(BaseUrl + "GetUserGroupList/1/", "GET", data, null, false))
                    .Result;
        }

        /// <summary>
        /// Resolves a vanity url into a SteamId64.
        /// </summary>
        /// <param name="vanityUrl">The vanity url part of the url (not whole url). ex: fatherfoxxy NOT https://steamcommunity.com/id/FatherFoxxy </param>
        /// <param name="urlType">
        /// 1 - (default) Individual profile
        /// 2 - Group Profile
        /// 3 - Offical Game Group Profile
        /// </param>
        /// <returns>A ResolveVanityUrlResult object.</returns>
        public ResolveVanityUrlResult ResolveVanityUrl(string vanityUrl, int urlType = 1)
        {
            var data = new Dictionary<string, string>
            {
                {"key", this.apiKey},
                {"vanityurl", vanityUrl},
                {"url_type", urlType.ToString()}
            };
            return
                JsonConvert.DeserializeObject<ResolveVanityUrlBaseResult>(Web.Fetch(BaseUrl + "ResolveVanityURL/v1/", "GET", data, null, false))
                    .Response;
        }

        private static string CommaDelimit(List<ulong> toDelimit)
        {
            string returned = toDelimit.Aggregate(string.Empty, (current, @ulong) => current + (@ulong + ","));
            return returned.Substring(0, returned.Length - 1);
        }
    }
}
