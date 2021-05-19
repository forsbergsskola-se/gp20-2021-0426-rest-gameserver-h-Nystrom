using System;
using System.Collections;
using System.Threading.Tasks;
using ClientApi;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests{
    public class ClientToRestApiTests
    {
        const string BaseUrl = "http://localhost:5001/api/mmorpg/";
        IClient client;
        
        [SetUp]
        public void SetUp(){
            client = new Client(BaseUrl);  
        }
        [UnityTest]
        public IEnumerator GetLeaderboardPlayersInJson()
        {
            const string uri = "players/leaderboard/";
            Task<string> webRequestTask;
            try{
                webRequestTask = client.GetRequest(uri);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsFaulted || webRequestTask.IsCompleted);
            Assert.AreNotEqual(string.Empty,webRequestTask.Result);
            Debug.Log(webRequestTask.Result);
        }
        [UnityTest]
        public IEnumerator GetPlayerInJson()
        {
            const string uri = "players/";
            Task<string> webRequestTask;
            try{
                webRequestTask = client.GetRequest(uri+Guid.Parse("6615f6de-b819-4e72-b93a-bd543d8d5c0b"));
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => webRequestTask.IsFaulted || webRequestTask.IsCompleted);
            Assert.AreNotEqual(string.Empty,webRequestTask.Result);
            Assert.AreEqual(true, webRequestTask.Result.Contains("UserName"));
            Debug.Log(webRequestTask.Result);
        }
    }
}
