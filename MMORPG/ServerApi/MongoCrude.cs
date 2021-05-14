using System;
using System.Linq;
using System.Threading.Tasks;
using MMORPG.ServerApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MMORPG.ServerApi{
    public class MongoCrude : IRepository{
        MongoClient mongoClient;
        IMongoDatabase mongoDatabase;
        const int timeout = 1000;
        public MongoCrude(string host){
            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(host));
            mongoClient = new MongoClient(mongoClientSettings);
            mongoDatabase = mongoClient.GetDatabase("mmorpg");
        }
        public Task<Player> Get(Guid id){
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAll(){
            throw new NotImplementedException();
        }
        public async Task<Player> Create(Player player){
            //TODO: Create a timeOut after x seconds if the server doesn't respond!
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>("users");
                var task = mongoCollection.InsertOneAsync(player);
                if (await Task.WhenAny(task, Task.Delay(timeout)) == task){
                    return player;
                }
                throw new Exception("Connection timeout!");//TODO:Fix!
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