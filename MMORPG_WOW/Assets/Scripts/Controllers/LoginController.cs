using System;
using System.Net.Mime;
using ClientApi;
using ClientApi.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controllers{
    public class LoginController : MonoBehaviour{
        //TODO: Implement ui class
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        [SerializeField] UnityEvent<IPlayer> sendPlayer = new UnityEvent<IPlayer>();
        [SerializeField] Text playerNameText;
        [SerializeField] int minNameSize = 5;
        IPlayer myPlayer;
        IClient client;
        bool IsNotEmpty => myPlayer != null;

        void Awake(){
            client = Client.NewClient(BaseUrl);
        }

        public async void LoginPlayer(){
            if(IsNotEmpty) 
                return;
            var userId = PlayerPrefs.GetString("UserId", "");
            Debug.Log("UserId: "+userId);
            if (!HasUserId(userId)){
                Debug.Log("Error: no userId");//TODO: Error message
                return;
            }
            try{
                var webRequestResponse = await client.GetWebRequest($"players/myplayer/{Guid.Parse(userId)}");
                myPlayer = JsonConvert.DeserializeObject<Player>(webRequestResponse);
                PlayerPrefs.SetString("UserName", myPlayer.Name);
                sendPlayer?.Invoke(myPlayer);
                gameObject.SetActive(false);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);//TODO: Error message
            }
        }
        public async void CreatePlayer(){
            if(IsNotEmpty) 
                return;
            if (playerNameText.text.Length <= minNameSize){
                Debug.Log($"Error: Min Name size: {minNameSize}");
                return;
            }
            var userId = PlayerPrefs.GetString("UserId", "");
            if (HasUserId(userId)){
                Debug.Log("Error: You can only have one player account");
                return;
            }
            try{
                var newPlayer = new Player{Name = playerNameText.text};
                var webRequestResponse = await client.PostWebRequest("players/create/",newPlayer);
                myPlayer = JsonConvert.DeserializeObject<Player>(webRequestResponse);
                PlayerPrefs.SetString("UserName", myPlayer.Name);
                PlayerPrefs.SetString("UserId", myPlayer.Id.ToString());
                Debug.Log(myPlayer.Id + myPlayer.Name);
                sendPlayer?.Invoke(myPlayer);
                gameObject.SetActive(false);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);//TODO: Error message
            }
        }
        static bool HasUserId(string userId){
            return userId != string.Empty;
        }
    }
}
