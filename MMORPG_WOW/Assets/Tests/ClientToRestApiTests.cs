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
        public IEnumerator GetLeader()
        {
            const string uri = "players/leaderboard/";
            Task<string> temp;
            try{
                temp = client.GetTargetObjects(uri);
            }
            catch (Exception e){
                Debug.Log(e);
                throw;
            }
            yield return new WaitUntil(() => temp.IsFaulted || temp.IsCompleted);
            Assert.AreNotEqual(string.Empty,temp.Result);
            Debug.Log(temp.Result);
        }
        // [UnityTest]
        // public IEnumerator GetPlayer()
        // {
        //     const string uri = "players/leaderboard/";
        //     Task<string> temp;
        //     try{
        //         temp = client.GetTargetObjects(uri);
        //     }
        //     catch (Exception e){
        //         Debug.Log(e);
        //         throw;
        //     }
        //     yield return new WaitUntil(() => temp.IsFaulted || temp.IsCompleted);
        //     Assert.AreNotEqual(string.Empty,temp.Result);
        //     Debug.Log(temp.Result);
        // }
    }
}
