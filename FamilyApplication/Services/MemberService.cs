using FamilyApplication.Models;
using FamilyApplication.DTOs;
using FamilyApplication.Repositories;
using FamilyApplication.Enums;

namespace FamilyApplication.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members = await _repository.GetAllAsync();
            return members.Select(m => MapToDto(m));
        }

        public async Task<MemberDto> GetMemberByIdAsync(long id)
        {
            var member = await _repository.GetByIdAsync(id);
            return member != null ? MapToDto(member) : null;
        }

        public async Task<MemberDto> CreateMemberAsync(CreateMemberDto createDto)
        {
            var member = new Member
            {
                MemberName = createDto.MemberName,
                Age = createDto.Age,
                familyCategory = createDto.FamilyCategory,
                BirthDate = createDto.BirthDate,
                Photo = createDto.Photo,
                FamilyId = createDto.FamilyId
            };

            var created = await _repository.CreateAsync(member);
            return MapToDto(created);
        }

        public async Task<MemberDto> UpdateMemberAsync(long id, UpdateMemberDto updateDto)
        {
            var existingMember = await _repository.GetByIdAsync(id);
            if (existingMember == null)
                return null;

            existingMember.MemberName = updateDto.MemberName ?? existingMember.MemberName;
            existingMember.Age = updateDto.Age ?? existingMember.Age;
            existingMember.familyCategory = updateDto.FamilyCategory ?? existingMember.familyCategory;
            existingMember.BirthDate = updateDto.BirthDate ?? existingMember.BirthDate;
            existingMember.Photo = updateDto.Photo ?? existingMember.Photo;

            var updated = await _repository.UpdateAsync(existingMember);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteMemberAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersByCategoryAsync(string category)
        {
            if (Enum.TryParse<FamilyCategory>(category, true, out var categoryEnum))
            {
                var members = await _repository.GetByCategoryAsync(categoryEnum);
                return members.Select(m => MapToDto(m));
            }
            return new List<MemberDto>();
        }

        private MemberDto MapToDto(Member member)
        {
            return new MemberDto
            {
                MemberId = member.MemberId,
                MemberName = member.MemberName,
                Age = member.Age,
                FamilyCategory = member.familyCategory.ToString(),
                BirthDate = member.BirthDate,
                Photo = member.Photo
            };
        }
    }
}