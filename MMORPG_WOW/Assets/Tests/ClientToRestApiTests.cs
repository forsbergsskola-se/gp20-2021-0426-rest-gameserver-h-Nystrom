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
        Guid createAndRemoveId;
        
        [SetUp]
        public void SetUp(){
            client = new Client(BaseUrl);
        }
        [UnityTest, Order(1)]
        public IEnumerator GetPlayerInJson()
        {
            const string uri = "players/myplayer/";
            var id = Guid.Parse("92dc2e76-da45-47e2-8084-48f24935f78c");
            Task<string> webRequestTask;
            try{
                webRequestTask = client.GetWebRequest(uri+id);
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
            Assert.AreEqual(id,player.Id);
            Assert.AreEqual("UserName", player.Name);
            Debug.Log(webRequestTask.Result);
        }
        [UnityTest, Order(2)]
        public IEnumerator ModifyPlayerToServerInJson()
        {
            const string uri = "players/modify/";
            Task<string> webRequestTask;
            var modifiedPlayer = new ModifiedPlayer{Id = player.Id, Score = 1};
            var oldScore = player.Score;
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
            Assert.AreEqual(oldScore+modifiedPlayer.Score,player.Score);
            Assert.AreEqual("UserName", player.Name);
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
            var newPlayer = new Player{Name = $"UnityClientTest({randomPlayerTestNameId})"};
            var oldPlayer = newPlayer;
            try{
                
                webRequestTask = client.PostWebRequest(uri,newPlayer);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsCompleted || webRequestTask.IsFaulted);
            try{
                newPlayer = JsonConvert.DeserializeObject<Player>(webRequestTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            Assert.AreEqual(oldPlayer.Name,newPlayer.Name);
            Assert.AreEqual(true, newPlayer.Id != Guid.Empty);
            Debug.Log(webRequestTask.Result);
            createAndRemoveId = newPlayer.Id;
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
