using FamilyApplication.Data;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.CreationDate = DateTime.Now;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null || user.DeletionDate.HasValue)
                return false;

            user.DeletionDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _context.User
                .Where(u => u.UserId == id && u.DeletionDate == null)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.User
                .Where(u => u.DeletionDate == null)
                .ToListAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
