using System;
using ClientApi.Models;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        const string BaseUrl = "https://localhost:5001/api/mmorpg/";
        Client client;
        [SerializeField] Player[] leaderboard;

        void Start(){
            client = new Client(BaseUrl);
        }
        public async void GetLeaderBoard(){
            const string uri = "players/leaderboard/";
            try{
                var jsonResponse = await client.GetTargetObjects(uri);
                Debug.Log(jsonResponse);
                leaderboard = JsonUtility.FromJson<Player[]>(jsonResponse);
                Debug.Log($"Success: {leaderboard.Length}");
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            
        }
    }
}
