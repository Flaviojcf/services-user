using UserEntity = Services.User.Domain.Entities.User;

namespace Services.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAsync(Guid id);
        Task AddAsync(UserEntity user);
        Task UpdateAsync(UserEntity user);
        Task DeleteAsync(Guid id);
    }
}
