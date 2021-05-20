using System;
using System.Collections.Generic;
using ClientApi.Models;
using ClientApi.UI;
using Newtonsoft.Json;
using UnityEngine;

namespace ClientApi.Controllers{
    public class LeaderBoardController : MonoBehaviour{
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        [SerializeField] LeaderBoardPlayerUi playerLeaderBoardUiPrefab;
        [SerializeField] Transform leaderBoardUiParent;
        [SerializeField] List<LeaderBoardPlayerUi> playerUiInstances = new List<LeaderBoardPlayerUi>();

        public async void GetLeaderBoard(){
            const string uri = "players/leaderboard/";
            var client = Client.NewClient(BaseUrl);
            DestroyChildren();
            try{
                var webRequestResponse = await client.GetWebRequest(uri);
                var leaderboard = JsonConvert.DeserializeObject<Player[]>(webRequestResponse);
                LeaderBoardSetUp(leaderboard);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message); //TODO: Error message
            }
        }
        public void Hide(){
            DestroyChildren();
            leaderBoardUiParent.gameObject.SetActive(false);
        }
        void LeaderBoardSetUp(IReadOnlyList<Player> leaderboard){
            for (var playerIndex = 0; playerIndex < leaderboard.Count; playerIndex++){
                var instance = Instantiate(playerLeaderBoardUiPrefab, leaderBoardUiParent);
                instance.SetUp(playerIndex + 1, leaderboard[playerIndex]);
                playerUiInstances.Add(instance);
            }
            leaderBoardUiParent.gameObject.SetActive(true);
        }
        void DestroyChildren(){
            foreach (var playerUi in playerUiInstances){
                playerUi.Destroy();
            }
            playerUiInstances.Clear();
        }
    }
}