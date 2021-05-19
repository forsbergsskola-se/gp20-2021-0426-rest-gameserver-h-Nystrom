using System;
using MMORPG.Models;
using MMORPG.ServerApi;
using MMORPG.ServerApi.ServerExceptions;
using NUnit.Framework;

namespace GameServerTests.MmoRpgTests{
    public class RepositoryLiveDataBaseTests{
        //TODO: Refactor duplication!
        
        IRepository<Player> mongoRepository;
        [SetUp]
        public void Setup(){
            mongoRepository = new MongoRepository<Player>();
        }
        [Test]
        public void AddNewPlayerToDataBase(){//TODO:Step one
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
        public void GetPlayerFromDataBase(){
            try{
                var playerId = Guid.Parse("6615f6de-b819-4e72-b93a-bd543d8d5c0b");
                var getPlayerTask = mongoRepository.Get(playerId);
                Console.WriteLine($"Result name: {getPlayerTask.Result.Name}");
                Assert.AreEqual("UserName", getPlayerTask.Result.Name);
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
                Assert.AreEqual("UserName", getPlayersTask.Result[0].Name);
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
                    Score = 2
                };
                var playerId = Guid.Parse("6615f6de-b819-4e72-b93a-bd543d8d5c0b");
                var getPlayerTask = mongoRepository.Get(playerId);
                getPlayerTask.Wait();
                var getModifiedPlayersTask = mongoRepository.Modify(getPlayerTask.Result.Id, modifiedPlayer);
                getModifiedPlayersTask.Wait();
                var resultScore = getPlayerTask.Result.Score + modifiedPlayer.Score;
                Assert.AreEqual(resultScore, getModifiedPlayersTask.Result.Score);
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
                Level = 1,
                IsDeleted = false,
                Score = 1,
                CreationTime = DateTime.Now.ToUniversalTime()
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