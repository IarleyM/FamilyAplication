using FamilyApplication.Data;
using FamilyApplication.Models;
using FamilyApplication.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FamilyGroupApplication.Repositories
{
    public class FamilyGroupRepository : IFamilyGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public FamilyGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FamilyGroup>> GetAllAsync()
        {
            return await _context.FamilyGroup
                .Where(m => m.DeletionDate == null) // Não deletados
                .ToListAsync();
        }

        public async Task<FamilyGroup> GetByIdAsync(long id)
        {
            return await _context.FamilyGroup
                .FirstOrDefaultAsync(m => m.FamilyGroupId == id && m.DeletionDate == null);
        }

        public async Task<FamilyGroup> CreateAsync(FamilyGroup FamilyGroup)
        {
            FamilyGroup.CreationDate = DateTime.Now;

            await _context.FamilyGroup.AddAsync(FamilyGroup);
            await _context.SaveChangesAsync();

            return FamilyGroup;
        }

        public async Task<FamilyGroup> UpdateAsync(FamilyGroup FamilyGroup)
        {
            _context.Entry(FamilyGroup).Property(x => x.CreationDate).IsModified = false;
            _context.FamilyGroup.Update(FamilyGroup);
            await _context.SaveChangesAsync();

            return FamilyGroup;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var FamilyGroup = await _context.FamilyGroup.FindAsync(id);
            if (FamilyGroup == null || FamilyGroup.DeletionDate.HasValue)
                return false;

            FamilyGroup.DeletionDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.FamilyGroup
                .AnyAsync(m => m.FamilyGroupId == id && m.DeletionDate == DateTime.MinValue);
        }
    }
}