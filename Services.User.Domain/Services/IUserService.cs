using UserEntity = Services.User.Domain.Entities.User;

namespace Services.User.Domain.Services
{
    public interface IUserService
    {
        Task<UserEntity> GetUserById(Guid Id);
    }
}
