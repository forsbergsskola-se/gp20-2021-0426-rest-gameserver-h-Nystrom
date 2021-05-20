using System.Collections.Generic;
using ClientApi.Models;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        [SerializeField] int testLevel;
        [SerializeField] int testScore;
        [SerializeField] List<Item> items = new List<Item>();
        IPlayer player;

        public void SetUp(IPlayer playerData){
            name = playerData.Name;
            player = playerData;
            testLevel = playerData.Level;
            testScore = playerData.Score;
            items = playerData.Items;
        }
    }
}