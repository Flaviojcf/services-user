using Services.User.Domain.Repositories;
using Services.User.Domain.Services;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.User.Application.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<UserEntity> GetUserById(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }
}
