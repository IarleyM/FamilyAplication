using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByIdAsync(long id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO user);
        Task<UserDTO> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(long id);
    }
}
