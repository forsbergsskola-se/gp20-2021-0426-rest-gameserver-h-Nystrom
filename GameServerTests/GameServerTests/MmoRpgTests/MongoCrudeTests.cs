using System;
using MMORPG.ServerApi;
using MMORPG.ServerApi.Models;
using NUnit.Framework;

namespace GameServerTests.MmoRpgTests{
    public class MongoCrudeTests{
        MongoCrude mongoCrude;
        [SetUp]
        public void Setup(){
            mongoCrude = new MongoCrude("mongodb://localhost:27017");
        }
        
        [Test]
        public void AddNewPlayerToDataBase(){
            var player = new Player{
                Name = "Hello",
                Level = 1,
                IsDeleted = false,
                Score = 1,
                CreationTime = DateTime.Now.ToUniversalTime()
            };
            var temp = mongoCrude.Create(player);
            Assert.AreEqual(player, temp.Result);
        }
    }
}