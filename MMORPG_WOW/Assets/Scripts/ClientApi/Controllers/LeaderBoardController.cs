using System;
using ClientApi.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace ClientApi.Controllers{
    public class LeaderBoardController : MonoBehaviour{
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        [SerializeField]UnityEvent<Player[]> sendLeaderboard = new UnityEvent<Player[]>(); 

        public async void GetLeaderBoard(){
            const string uri = "players/leaderboard/";
            var client = Client.NewClient(BaseUrl);
            try{
                var webRequestResponse = await client.GetWebRequest(uri);
                var leaderboard = JsonConvert.DeserializeObject<Player[]>(webRequestResponse);
                Debug.Log($"Success: {leaderboard.Length}");
                Debug.Log($"Name of player one: {leaderboard[0].Name}");
                sendLeaderboard?.Invoke(leaderboard);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);
                //TODO: Error message
            }
        }
    }
}