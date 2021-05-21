using System;
using System.Collections.Generic;
using ClientApi.Broker;
using ClientApi.Broker.Messages;
using ClientApi.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        DateTime startDateTime;
        IPlayer player;
        IClient client;

        void Awake(){
            client = Client.NewClient(BaseUrl);
            EventBroker.Instance().SubscribeMessage<ModifiedPlayer>(ModifyPlayer);   
        }

        void OnDestroy(){
            EventBroker.Instance().UnsubscribeMessage<ModifiedPlayer>(ModifyPlayer);   
        }
        public void SetUp(IPlayer playerData){
            name = playerData.Name;
            player = playerData;
            EventBroker.Instance().SendMessage(new UpdateHudMessage(player.Gold, player.Xp));
            GetCurrentServerDateTime();
            SendQuestRequest();
        }

        async void SendQuestRequest(){
            var uri = $"Items/newquest/{player.Id}";
            if (player.Items != null && !HasNoQuest(player.Items)) 
                return;
            
            try{
                var webRequestResponse = await client.GetWebRequest(uri);
                player = JsonConvert.DeserializeObject<Player>(webRequestResponse);
                Debug.Log(player.Items.Count);
                foreach (var item in player.Items){
                    Debug.Log(item.Name + item.ItemType);
                }
            }
            catch (Exception e){
                Debug.Log("SendQuestRequest: " + e.GetBaseException().Message);
            }
        }

        static bool HasNoQuest(IEnumerable<Item> items){
            foreach (var item in items){
                if (item.Name == "Quest"){
                    return false;
                }
            }
            return true;
        }

        async void GetCurrentServerDateTime(){
            const string uri = "serverdatetime/current/";
            try{
                var webRequestResult = await client.GetWebRequest(uri);
                startDateTime = JsonConvert.DeserializeObject<DateTime>(webRequestResult);
                Debug.Log($"Server DateTime: {startDateTime}");
            }
            catch (Exception e){
                Debug.Log(e);//TODO: Throw error
            }
        }
        async void ModifyPlayer(ModifiedPlayer modifiedPlayer){
            const string uri = "players/modify/";
            try{
                modifiedPlayer.Id = player.Id;
                var webRequestResponse = await client.PostWebRequest(uri+player.Id,modifiedPlayer);
                Debug.Log("Response: "+webRequestResponse);
                player = JsonConvert.DeserializeObject<Player>(webRequestResponse);
                EventBroker.Instance().SendMessage(new UpdateHudMessage(player.Gold, player.Xp));
            }
            catch (Exception e){
                Debug.Log(e.GetBaseException().Message);//TODO: Error message
            }
        }
        void RequestCurrentTime(){
            throw new NotImplementedException();
        }
        void RequestNewQuest(){
            throw new NotImplementedException();
        }
        void CompleteQuest(){
            throw new NotImplementedException();
        }
    }
}