using FamilyApplication.Enums;
using System.ComponentModel.DataAnnotations;

namespace FamilyApplication.DTOs
{
    public class MemberDto
    {
        public long MemberId { get; set; }
        public string MemberName { get; set; }
        public int Age { get; set; }
        public string FamilyCategory { get; set; } 
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public long FamilyId { get; set; }
    }

    public class CreateMemberDto
    {
        public string MemberName { get; set; }
        public int Age { get; set; }
        public FamilyCategory FamilyCategory { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }

        [Required]
        public long FamilyId { get; set; }
    }

    public class UpdateMemberDto
    {
        public string? MemberName { get; set; }
        public int? Age { get; set; }
        public FamilyCategory? FamilyCategory { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Photo { get; set; }
    }
}