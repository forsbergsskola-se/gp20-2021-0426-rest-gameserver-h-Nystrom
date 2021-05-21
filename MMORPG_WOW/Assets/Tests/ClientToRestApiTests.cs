using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using ClientApi;
using ClientApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

namespace Tests{
    public class ClientToRestApiTests
    {
        //TODO: Refactor and remove duplication!
        
        
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        IClient client;
        Player player;
        const string TestPlayerName = "ServerTestPlayer";

        [UnitySetUp, Order(0)]
        public IEnumerator SetupAndCreatTestPlayer(){
            client = Client.NewClient(BaseUrl);
            
            const string uri = "players/create/";
            Task<string> webRequestTask;
            var randomPlayerTestNameId = Random.Range(0, 1000);
            var createdPlayer = new NewPlayer{Name = $"UnityClientTest({randomPlayerTestNameId})"};
            try{
                
                webRequestTask = client.PostWebRequest(uri,createdPlayer);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsCompleted || webRequestTask.IsFaulted);
            try{
                player = JsonConvert.DeserializeObject<Player>(webRequestTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            Assert.AreEqual(createdPlayer.Name,player.Name);
            Assert.AreEqual(true, player.Id != Guid.Empty);
            Debug.Log(webRequestTask.Result);
        }

        [UnityTest, Order(1)]
        public IEnumerator GetPlayerInJson()
        {
            const string uri = "players/myplayer/";
            IPlayer tempPlayer;
            Task<string> webRequestTask;
            try{
                webRequestTask = client.GetWebRequest(uri+player.Id);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsFaulted || webRequestTask.IsCompleted);
            try{
                tempPlayer = JsonConvert.DeserializeObject<Player>(webRequestTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            Assert.AreEqual(player.Id,tempPlayer.Id);
            Assert.AreEqual(player.Name, tempPlayer.Name);
            Assert.AreEqual(player.Gold, tempPlayer.Gold);
            Assert.AreEqual(player.Xp, tempPlayer.Xp);
            Assert.AreEqual(player.CreationTime.ToString(CultureInfo.InvariantCulture), tempPlayer.CreationTime.ToString(CultureInfo.InvariantCulture));
            Debug.Log(webRequestTask.Result);
        }
        [UnityTest, Order(2)]
        public IEnumerator ModifyPlayerToServerInJson()
        {
            const string uri = "players/modify/";
            Task<string> webRequestTask;
            var modifiedPlayer = new ModifiedPlayer{Id = player.Id, Xp = 1, Gold = 10};
            IPlayer tempPlayer;
            try{
                webRequestTask = client.PostWebRequest(uri+player.Id,modifiedPlayer);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsCompleted || webRequestTask.IsFaulted);
            try{
               tempPlayer = JsonConvert.DeserializeObject<Player>(webRequestTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            Assert.AreEqual(player.Xp+modifiedPlayer.Xp,tempPlayer.Xp);
            Assert.AreEqual(player.Gold+modifiedPlayer.Gold, tempPlayer.Gold);
            Assert.AreEqual(player.Name, tempPlayer.Name);
            Assert.AreEqual(player.Id, tempPlayer.Id);
            Debug.Log(webRequestTask.Result);
        }
        [UnityTest, Order(3)]
        public IEnumerator GetLeaderboardPlayersInJson()
        {
            const string uri = "players/leaderboard/";
            Task<string> webRequestTask;
            try{
                webRequestTask = client.GetWebRequest(uri);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsFaulted || webRequestTask.IsCompleted);
            Assert.AreNotEqual(string.Empty,webRequestTask.Result);
            Debug.Log(webRequestTask.Result);
        }
        [UnityTest, Order(4)]
        public IEnumerator DeletePlayerFromDatabase(){
            const string uri = "players/delete/";
            Task<string> webRequestTask;
            Player deletedPlayer;
            try{
                webRequestTask = client.DeleteTargetObject(uri+player.Id);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsCompleted || webRequestTask.IsFaulted);
            try{
                deletedPlayer = JsonConvert.DeserializeObject<Player>(webRequestTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            Assert.AreEqual(true,deletedPlayer.IsDeleted);
            Assert.AreEqual(true, deletedPlayer.Id != Guid.Empty);
            Debug.Log(webRequestTask.Result);
        }
    }
}
