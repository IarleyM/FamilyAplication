using Microsoft.EntityFrameworkCore;
using FamilyApplication.Data;
using FamilyApplication.Models;
using FamilyApplication.Enums;

namespace FamilyApplication.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly ApplicationDbContext _context;

        public FamilyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Family>> GetAllAsync()
        {
            return await _context.Family
                .Where(m => m.DeletionDate == null) // Não deletados
                .ToListAsync();
        }

        public async Task<Family> GetByIdAsync(long id)
        {
            return await _context.Family
                .FirstOrDefaultAsync(m => m.FamilyId == id && m.DeletionDate == null);
        }

        public async Task<Family> CreateAsync(Family family)
        {
            family.CreationDate = DateTime.UtcNow;

            await _context.Family.AddAsync(family);
            await _context.SaveChangesAsync();

            return family;
        }

        public async Task<Family> UpdateAsync(Family family)
        {
            _context.Entry(family).Property(x => x.CreationDate).IsModified = false;
            _context.Family.Update(family);
            await _context.SaveChangesAsync();

            return family;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var family = await _context.Family.FindAsync(id);
            if (family == null || family.DeletionDate.HasValue)
                return false;

            // Soft delete
            family.DeletionDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Family
                .AnyAsync(m => m.FamilyId == id && m.DeletionDate == DateTime.MinValue);
        }
    }
}