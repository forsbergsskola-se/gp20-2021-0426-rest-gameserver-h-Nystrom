using System;
using System.Threading.Tasks;
using MMORPG.ServerApi.ServerExceptions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MMORPG.ServerApi{
    public class MongoRepository<TObject>  : IRepository<TObject> where TObject : IRequestObject{
        //TODO: Refactor duplication!
        //TODO: Set data when instantiating(collectionName, Host, DbName).

        readonly IMongoDatabase mongoDatabase;
        const int Timeout = 1000;
        readonly string collectionName;

        public MongoRepository(){
            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://localhost:27017"));
            var mongoClient = new MongoClient(mongoClientSettings);
            mongoDatabase = mongoClient.GetDatabase("game");
            collectionName = $"{typeof(TObject).Name.ToLower()}s";
        }
        public async Task<TObject> Get(Guid id){
            try{
                var mongoCollection = mongoDatabase.GetCollection<TObject>(collectionName);
                var targetObjectTask = mongoCollection.FindAsync(targetObject => targetObject.Id == id);

                if (await Task.WhenAny(targetObjectTask, Task.Delay(Timeout)) != targetObjectTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                
                if (targetObjectTask.Result == null)
                    throw new NotFoundException("404: Player not found!");
                return targetObjectTask.Result.First();
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw new NotFoundException("404: Player not found!");
            }
        }
        public async Task<TObject[]> GetAll(){
            try{
                var mongoCollection = mongoDatabase.GetCollection<TObject>(collectionName);
                var targetObjectTask = mongoCollection.Find(new BsonDocument()).ToListAsync();
                if (await Task.WhenAny(targetObjectTask, Task.Delay(Timeout)) != targetObjectTask)
                    throw new RequestTimeoutException("408: Request timeout!");

                if (targetObjectTask.Result == null)
                    throw new NotFoundException("404: Player not found!");
                return targetObjectTask.Result.ToArray();
            }
            catch (Exception e){
                throw e.GetBaseException();
            }
        }

        public async Task<TObject> Create(TObject targetObject){
            try{
                var mongoCollection = mongoDatabase.GetCollection<TObject>(collectionName);
                var targetObjectTask = mongoCollection.InsertOneAsync(targetObject);
                if (await Task.WhenAny(targetObjectTask, Task.Delay(Timeout)) == targetObjectTask){
                    return targetObject;
                }
                throw new RequestTimeoutException("408: Request timeout!");
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<TObject> Modify<TObject2>(Guid id, TObject2 targetObject) where TObject2 : IRequestObject{
            try{
                var mongoCollection = mongoDatabase.GetCollection<TObject>(collectionName);
                var updateTargetObject = Builders<TObject>.Update.Inc("Score", targetObject.Score);
                var targetObjectTask = mongoCollection.FindOneAndUpdateAsync(playerTarget => playerTarget.Id == id, updateTargetObject);
                
                if (await Task.WhenAny(targetObjectTask, Task.Delay(Timeout)) != targetObjectTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                if(targetObjectTask.Result == null)
                    throw new NotFoundException($"404: {typeof(TObject).Name} was not found!");
                targetObjectTask.Result.Score += targetObject.Score;
                return targetObjectTask.Result;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<TObject> Delete(Guid id){
            try{
                var mongoCollection = mongoDatabase.GetCollection<TObject>(collectionName);
                var targetObjectTask = mongoCollection.FindOneAndDeleteAsync(player => player.Id == id);
                
                if (await Task.WhenAny(targetObjectTask, Task.Delay(Timeout)) != targetObjectTask)
                    throw new RequestTimeoutException("408: Request timeout!");
                if(targetObjectTask.Result == null)
                    throw new NotFoundException($"404: {typeof(TObject).Name} was not found!");
                return targetObjectTask.Result;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}