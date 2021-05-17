using System;
using System.Threading.Tasks;
using MMORPG.Models;
using MMORPG.ServerApi.ServerExceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.ServerApi{
    public class MongoRepository : IRepository{
        //TODO: Refactor duplication!
        //TODO: Set data when instantiating(collectionName, Host, DbName).

        readonly IMongoDatabase mongoDatabase;
        const int Timeout = 1000;
        const string CollectionName = "players";

        public MongoRepository(){
            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
            var mongoClient = new MongoClient(mongoClientSettings);
            mongoDatabase = mongoClient.GetDatabase("game");
        }
        public async Task<Player> Get(Guid id){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(CollectionName);
                var getPlayerTask = mongoCollection.FindAsync(player1 => player1.Id == id);

                if (await Task.WhenAny(getPlayerTask, Task.Delay(Timeout)) != getPlayerTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                
                if (getPlayerTask.Result == null)
                    throw new NotFoundException("404: Player not found!");
                return getPlayerTask.Result.First();
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new NotFoundException("404: Player not found!");
            }
        }

        public async Task<Player[]> GetAll(){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(CollectionName);
                var getPlayerTask = mongoCollection.Find(new BsonDocument()).ToListAsync();

                if (await Task.WhenAny(getPlayerTask, Task.Delay(Timeout)) != getPlayerTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                
                if (getPlayerTask.Result == null)
                    throw new NotFoundException("404: Player not found!");
                return getPlayerTask.Result.ToArray();
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Player> Create(Player player){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(CollectionName);
                var task = mongoCollection.InsertOneAsync(player);
                if (await Task.WhenAny(task, Task.Delay(Timeout)) == task){
                    return player;
                }
                throw new RequestTimeoutException("408: Request timeout!");
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<Player> Modify(Guid id, ModifiedPlayer player){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(CollectionName);
                var updatePlayer = Builders<Player>.Update.Inc("Score", player.Score);
                var getPlayerTask = mongoCollection.FindOneAndUpdateAsync(playerTarget => playerTarget.Id == id, updatePlayer);
                
                if (await Task.WhenAny(getPlayerTask, Task.Delay(Timeout)) != getPlayerTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                if(getPlayerTask.Result == null)
                    throw new NotFoundException("404: Player not found!");
                getPlayerTask.Result.Score += player.Score;
                return getPlayerTask.Result;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<Player> Delete(Guid id){
            try{
                var mongoCollection = mongoDatabase.GetCollection<Player>(CollectionName);
                var getPlayerTask = mongoCollection.FindOneAndDeleteAsync(player => player.Id == id);
                
                if (await Task.WhenAny(getPlayerTask, Task.Delay(Timeout)) != getPlayerTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                if(getPlayerTask.Result == null)
                    throw new NotFoundException("404: No player was found!");
                return getPlayerTask.Result;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}