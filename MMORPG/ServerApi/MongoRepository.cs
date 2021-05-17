using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.ServerApi.Models;
using MMORPG.ServerApi.ServerExceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MMORPG.ServerApi{
    public class MongoRepository : IRepository{
        MongoClient mongoClient;
        IMongoDatabase mongoDatabase;
        const int timeout = 1000;
        string collectionName;
        public MongoRepository(string host, string databaseName, string collectionName){
            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(host));
            mongoClient = new MongoClient(mongoClientSettings);
            mongoDatabase = mongoClient.GetDatabase(databaseName);
            this.collectionName = collectionName;
        }
        public async Task<Player> Get(Guid id){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(collectionName);
                var getPlayerTask = mongoCollection.Find(new BsonDocument()).ToListAsync();

                if (await Task.WhenAny(getPlayerTask, Task.Delay(timeout)) != getPlayerTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                
                if (getPlayerTask.Result == null)
                    throw new NotFoundException("404: Players not found!");
                
                foreach (var player in getPlayerTask.Result.Where(player => player.Id == id)){
                    return player;
                }
                throw new NotFoundException("404: Player not found!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Player[]> GetAll(){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(collectionName);
                var getPlayerTask = mongoCollection.Find(new BsonDocument()).ToListAsync();

                if (await Task.WhenAny(getPlayerTask, Task.Delay(timeout)) != getPlayerTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                
                if (getPlayerTask.Result == null)
                    throw new NotFoundException("404: No players were found!");
                return getPlayerTask.Result.ToArray();
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Player> Create(Player player){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(collectionName);
                var task = mongoCollection.InsertOneAsync(player);
                if (await Task.WhenAny(task, Task.Delay(timeout)) == task){
                    return player;
                }
                throw new RequestTimeoutException("408: Request timeout!");
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new Exception(e.GetBaseException().Message);
            }
        }
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
            throw new NotImplementedException();
        }

        public Task<Player> Delete(Guid id){
            throw new NotImplementedException();
        }
    }
}