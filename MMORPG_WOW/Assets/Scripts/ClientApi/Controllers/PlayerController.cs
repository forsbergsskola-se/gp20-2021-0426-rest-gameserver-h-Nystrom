using System.Collections.Generic;
using ClientApi.Models;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        [SerializeField] string testName;
        [SerializeField] int testLevel;
        [SerializeField] int testScore;
        [SerializeField] List<Item> items;
        IPlayer player;

        public void SetUp(IPlayer playerData){
            player = playerData;
            testName = playerData.Name;
            testLevel = playerData.Level;
            testScore = playerData.Score;
            items = playerData.Items;
        }
    }
}