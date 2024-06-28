using MongoDB.Driver;
using Services.User.Domain.Repositories;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.User.Infrastructure.Persistance.MongoDb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _collection;
        public UserRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<UserEntity>("users");
        }
        public async Task AddAsync(UserEntity user)
        {
            await _collection.InsertOneAsync(user);
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await _collection.Find(u => u.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(UserEntity user)
        {
            await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}
