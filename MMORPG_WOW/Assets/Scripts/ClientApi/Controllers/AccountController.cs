using System;
using ClientApi.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ClientApi.Controllers{
    public class AccountController : MonoBehaviour{
        //TODO: Implement ui class
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        [SerializeField] UnityEvent deletionEvent = new UnityEvent();
        [SerializeField] Text playerNameText;
        [SerializeField] int minNameSize = 5;
        [SerializeField] PlayerController playerPrefab;
        IClient client;

        void Awake(){
            client = Client.NewClient(BaseUrl);
        }
        public async void LoginPlayer(){
            var userId = PlayerPrefs.GetString("UserId", "");
            Debug.Log("UserId: "+userId);
            if (!HasUserId(userId)){
                Debug.Log("Error: no userId");//TODO: Error message
                return;
            }
            try{
                var webRequestResponse = await client.GetWebRequest($"players/myplayer/{Guid.Parse(userId)}");
                var player = JsonConvert.DeserializeObject<Player>(webRequestResponse);
                InitialisePlayer(player);
                gameObject.SetActive(false);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);//TODO: Error message
            }
        }

        void InitialisePlayer(IPlayer player){
            var instance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            instance.SetUp(player);
        }

        public async void CreatePlayer(){
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
                var player = JsonConvert.DeserializeObject<Player>(webRequestResponse);
                PlayerPrefs.SetString("UserName", player.Name);
                PlayerPrefs.SetString("UserId", player.Id.ToString());
                InitialisePlayer(player);
                gameObject.SetActive(false);
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);//TODO: Error message
            }
        }

        public async void DeletePlayer(){
            var userId = PlayerPrefs.GetString("UserId", "");
            if (!HasUserId(userId)){
                Debug.Log("Error: You can only have one player account");
                return;
            }
            try{
                var webRequestResponse = await client.DeleteTargetObject($"players/delete/{Guid.Parse(userId)}");
                var deletedPlayer = JsonConvert.DeserializeObject<Player>(webRequestResponse);

                if (!deletedPlayer.IsDeleted){
                    Debug.Log("Error: something went wrong..");
                    return;
                }
                ResetUserData();
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);//TODO: Error message
            }
        }
        public void QuitLocally(){
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
        void ResetUserData(){
            PlayerPrefs.DeleteKey("UserId");
            PlayerPrefs.DeleteKey("UserName");
            playerNameText.text = "";
            deletionEvent?.Invoke();
        }
        static bool HasUserId(string userId){
            return userId != string.Empty;
        }
    }
}
