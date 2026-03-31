using FamilyApplication.DTOs;
using FamilyApplication.Models;
using FamilyApplication.Repositories;

namespace FamilyApplication.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createuser)
        {
            var user = new User
            {
                UserName = createuser.UserName,
                Email = createuser.Email,
                Password = createuser.Password,
                Phone = createuser.Phone,
                BirthDate = createuser.BirthDate,
                CreationDate = DateTime.Now,
                DeletionDate = null
            };

            var createdUser = await _userRepository.CreateUserAsync(user);
            return MapToDto(createdUser);
        }


        public Task<bool> DeleteUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserByIdAsync(long id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return null;

            return MapToDto(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var Users = await _userRepository.GetUsersAsync();
            return Users.Select(u => MapToDto(u));
        }

        public Task<UserDTO> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        private UserDTO MapToDto(User createdUser)
        {
            return new UserDTO 
            {
                UserId = createdUser.UserId,
                UserName = createdUser.UserName,
                Email = createdUser.Email,
                Phone = createdUser.Phone,
                BirthDate = createdUser.BirthDate
            };
        }
    }
}
