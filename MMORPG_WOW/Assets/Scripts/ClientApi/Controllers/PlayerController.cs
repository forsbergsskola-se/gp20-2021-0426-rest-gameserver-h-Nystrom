using System.Collections.Generic;
using ClientApi.Models;
using UnityEngine;

namespace ClientApi.Controllers{
    public class PlayerController : MonoBehaviour{
        [SerializeField] int testXp;
        [SerializeField] int testScore;
        [SerializeField] List<Item> items = new List<Item>();
        IPlayer player;

        public void SetUp(IPlayer playerData){
            name = playerData.Name;
            player = playerData;
            testXp = playerData.Xp;
            testScore = playerData.Gold;
            items = playerData.Items;
        }
    }
}