using System;
using MMORPG.ServerApi;
using MMORPG.ServerApi.Models;
using MMORPG.ServerApi.ServerExceptions;
using MongoDB.Bson;
using NUnit.Framework;

namespace GameServerTests.MmoRpgTests{
    public class RepositoryTests{
        IRepository mongoRepository;
        [SetUp]
        public void Setup(){
            mongoRepository = new MongoRepository("mongodb://localhost:27017", "game", "players");
        }
        [Test]
        public void AddNewPlayerToDataBaseHandlesCallTimeouts(){
            var player = new Player{
                Name = "UserName",
                Level = 1,
                IsDeleted = false,
                Score = 1,
                CreationTime = DateTime.Now.ToUniversalTime()
            };
            try{
                var creatPlayerTask = mongoRepository.Create(player);
                Console.WriteLine(creatPlayerTask.Result.Id);
                Assert.AreEqual(player, creatPlayerTask.Result);
            }
            catch (Exception e){
                Console.WriteLine(e);
                Assert.Fail(e.GetBaseException().Message);
            }
        }
        [Test]
        public void GetPlayerFromDataBase(){//TODO: Fix!
            try{
                var playerId = Guid.Parse("13295692-779b-49e4-afb9-5082c184d136");
                var player = mongoRepository.Get(playerId);
                Console.WriteLine($"Result name: {player.Result.Name}");
                Assert.AreEqual("UserName", player.Result.Name);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                Assert.Fail(e.GetBaseException().Message);
            }
        }
        [Test]
        public void GetAllPlayersFromDataBase(){
            try{
                var player = mongoRepository.GetAll();
                Console.WriteLine($"Result name: {player.Result[0].Name}");
                Assert.AreEqual("UserName", player.Result[0].Name);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                Assert.Fail(e.GetBaseException().Message);
            }
        }
    }
}