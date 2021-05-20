using System;
using ClientApi.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        IClient client;
        [SerializeField] Player[] leaderboard;

        void Start(){
            client = new Client(BaseUrl);
        }
        public async void GetLeaderBoard(){
            const string uri = "players/leaderboard/";
            try{
                var webRequestTask = await client.GetWebRequest(uri);
                leaderboard = JsonConvert.DeserializeObject<Player[]>(webRequestTask);
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
