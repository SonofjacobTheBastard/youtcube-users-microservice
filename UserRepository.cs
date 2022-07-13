using MongoDB.Driver;
using YoutCubeUsersMicroservice.Models;

namespace YoutCubeUsersMicroservice
{
    public class UserRepository
    {
        private const string collectionName = "users";
        private IMongoCollection<User> dbCollection;
        private FilterDefinitionBuilder<User> filterBuilder;

        public UserRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Users");
            dbCollection = database.GetCollection<User>(collectionName);
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            FilterDefinition<User> filter = filterBuilder.Eq(user => user.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user cannot be null");

            await dbCollection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user cannot be null");

            FilterDefinition<User> filter = filterBuilder.Eq(existing => existing.Id, user.Id);
            await dbCollection.ReplaceOneAsync(filter, user);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<User> filter = filterBuilder.Eq(user => user.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}