using System;
using ClientApi.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        IClient client;
        [SerializeField] Player[] leaderboard;
        IPlayer myPlayer;

        void Start(){
            client = new Client(BaseUrl);
        }

        public async void GetOrCreateAccount(){
            if(myPlayer != null) 
                return;
            var userId = PlayerPrefs.GetString("UserId", "");
            try{
                if (userId == ""){
                   var postWebRequestResponse = await client.PostWebRequest("players/create/", new Player{Name = "MyPlayerName"});
                    myPlayer = JsonConvert.DeserializeObject<Player>(postWebRequestResponse);
                    PlayerPrefs.SetString("UserId", myPlayer.Id.ToString());
                    Debug.Log($"Created player: Name: {myPlayer.Name}, Id: {myPlayer.Id}, CreationTime: {myPlayer.CreationTime}");
                    return;
                }
                var getWebRequestResponse = await client.GetWebRequest($"players/myplayer/{Guid.Parse(userId)}");
                myPlayer = JsonConvert.DeserializeObject<Player>(getWebRequestResponse);
                Debug.Log($"Get player: Name: {myPlayer.Name}, Id: {myPlayer.Id}, CreationTime: {myPlayer.CreationTime}");
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);
                //TODO: Error message
            }
            
        }
        public async void GetLeaderBoard(){
            const string uri = "players/leaderboard/";
            try{
                var webRequestResponse = await client.GetWebRequest(uri);
                leaderboard = JsonConvert.DeserializeObject<Player[]>(webRequestResponse);
                Debug.Log($"Success: {leaderboard.Length}");
                Debug.Log($"Name of player one: " + leaderboard[0].Name);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);
                //TODO: Error message
            }
        }
    }
}
