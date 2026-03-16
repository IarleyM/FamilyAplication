using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> GetMemberByIdAsync(long id);
        Task<MemberDto> CreateMemberAsync(CreateMemberDto member);
        Task<MemberDto> UpdateMemberAsync(long id, UpdateMemberDto member);
        Task<bool> DeleteMemberAsync(long id);
        //Task<bool> ExistsMemberAsync(long id);
        Task<IEnumerable<MemberDto>> GetMembersByCategoryAsync(string   category);
    }
}
