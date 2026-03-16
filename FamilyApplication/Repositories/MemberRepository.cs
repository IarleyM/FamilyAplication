using Microsoft.EntityFrameworkCore;
using FamilyApplication.Data;
using FamilyApplication.Models;
using FamilyApplication.Enums;

namespace FamilyApplication.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members
                .Where(m => m.DeletionDate == null) // Não deletados
                .ToListAsync();
        }

        public async Task<Member> GetByIdAsync(long id)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.MemberId == id && m.DeletionDate == null);
        }

        public async Task<Member> CreateAsync(Member member)
        {
            member.CreationDate = DateTime.Now;

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<Member> UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return false;

            // Soft delete
            member.DeletionDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Members
                .AnyAsync(m => m.MemberId == id && m.DeletionDate == DateTime.MinValue);
        }

        public async Task<IEnumerable<Member>> GetByCategoryAsync(FamilyCategory category)
        {
            return await _context.Members
                .Where(m => m.familyCategory == category && m.DeletionDate == null)
                .ToListAsync();
        }
    }
}