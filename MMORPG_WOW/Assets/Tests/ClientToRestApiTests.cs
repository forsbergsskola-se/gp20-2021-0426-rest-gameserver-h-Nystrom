using System;
using System.Collections;
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
        string testPlayerName = "ServerTestPlayer";
        Guid testPlayerId;
        Guid createAndRemoveId;
        
        [SetUp]
        public void SetUp(){
            client = Client.NewClient(BaseUrl);
            testPlayerId = Guid.Parse("6c20f3f5-4810-482f-aa7c-95401e40ab8b");
        }
        [UnityTest, Order(1)]
        public IEnumerator GetPlayerInJson()
        {
            const string uri = "players/myplayer/";
            Task<string> webRequestTask;
            try{
                webRequestTask = client.GetWebRequest(uri+testPlayerId);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsFaulted || webRequestTask.IsCompleted);
            try{
                player = JsonConvert.DeserializeObject<Player>(webRequestTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            Assert.AreEqual(testPlayerId,player.Id);
            Assert.AreEqual(testPlayerName, player.Name);
            Debug.Log(webRequestTask.Result);
        }
        [UnityTest, Order(2)]
        public IEnumerator ModifyPlayerToServerInJson()
        {
            const string uri = "players/modify/";
            Task<string> webRequestTask;
            var modifiedPlayer = new ModifiedPlayer{Id = player.Id, Xp = 1, Gold = 10};
            var addedXp = player.Xp;
            var addedGold = player.Gold;
            try{
                webRequestTask = client.PostWebRequest(uri+player.Id,modifiedPlayer);
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
            Assert.AreEqual(addedXp+modifiedPlayer.Xp,player.Xp);
            Assert.AreEqual(testPlayerName, player.Name);
            Assert.AreEqual(modifiedPlayer.Id, player.Id);
            Debug.Log(webRequestTask.Result);
        }
        
        [UnityTest]
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
        
        [UnityTest, Order(3)]
        public IEnumerator CreateNewPlayerInDatabase()
        {
            const string uri = "players/create/";
            Task<string> webRequestTask;
            var randomPlayerTestNameId = Random.Range(0, 1000);
            var createdPlayer = new NewPlayer{Name = $"UnityClientTest({randomPlayerTestNameId})"};
            IPlayer player;
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
            createAndRemoveId = player.Id;
        }

        [UnityTest, Order(4)]
        public IEnumerator DeletePlayerFromDatabase(){
            const string uri = "players/delete/";
            Task<string> webRequestTask;
            Player deletedPlayer;
            try{
                webRequestTask = client.DeleteTargetObject(uri+createAndRemoveId);
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
