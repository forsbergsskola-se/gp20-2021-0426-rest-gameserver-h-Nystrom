using System;
using MMORPG.Models;
using MMORPG.ServerApi;
using MMORPG.ServerApi.ServerExceptions;
using NUnit.Framework;

namespace GameServerTests.MmoRpgTests{
    public class RepositoryLiveDataBaseTests{
        //TODO: Refactor duplication!
        const string TestPlayerName = "ServerTestPlayer";
        IRepository<Player> mongoRepository;
        Guid testPlayerId;
        
        [SetUp]
        public void Setup(){
            mongoRepository = new MongoRepository<Player>();
            testPlayerId = Guid.Parse("6c20f3f5-4810-482f-aa7c-95401e40ab8b");
        }
        [Test]
        public void AddNewPlayerToDataBase(){//TODO:Step one
            var player = new Player{
                Name = TestPlayerName,
                IsDeleted = false,
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
        public void GetPlayerFromDataBase(){
            try{
                var playerId = testPlayerId;
                var getPlayerTask = mongoRepository.Get(playerId);
                Console.WriteLine($"Result name: {getPlayerTask.Result.Name}");
                Assert.AreEqual(TestPlayerName, getPlayerTask.Result.Name);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                Assert.Fail(e.GetBaseException().Message);
            }
        }
        [Test]
        public void GetAllPlayersInDataBase(){
            try{
                var getPlayersTask = mongoRepository.GetAll();
                Console.WriteLine($"Total players: {getPlayersTask.Result.Length}");
                for (var i = 0; i < getPlayersTask.Result.Length; i++){
                    Console.WriteLine($"({i}) {getPlayersTask.Result[i].Name}");
                }
                Assert.AreEqual(TestPlayerName, getPlayersTask.Result[0].Name);
                Assert.Less(0,getPlayersTask.Result.Length);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                Assert.Fail(e.GetBaseException().Message);
            }
        }

        [Test]
        public void GetPlayerAndModifyInDataBase(){
            try{
                var modifiedPlayer = new ModifiedPlayer{
                    Xp = 2
                };
                
                var getModifiedPlayersTask = mongoRepository.Modify(testPlayerId, nameof(modifiedPlayer.Xp), modifiedPlayer.Xp);
                getModifiedPlayersTask.Wait();
                var getPlayerTask = mongoRepository.Get(testPlayerId);
                getPlayerTask.Wait();
                var resultScore = modifiedPlayer.Xp + getModifiedPlayersTask.Result.Xp;
                Assert.AreEqual(resultScore, getPlayerTask.Result.Xp);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                Assert.Fail(e.GetBaseException().Message);
            }
        }

        [Test]
        public void CreateAndRemovePlayerFromDataBase(){
            var player = new Player{
                Name = "CreateThenRemove",
            };
            try{
                var createPlayerTask = mongoRepository.Create(player);
                var removePlayersTask = mongoRepository.Delete(createPlayerTask.Result.Id);
                mongoRepository.Get(removePlayersTask.Result.Id);
            }
            catch (NotFoundException e){
                Assert.Pass(e.Message);
            }
            catch (Exception e){
                Console.WriteLine(e.GetBaseException().Message);
                Assert.Fail(e.GetBaseException().Message);
            }
        }
    }
}