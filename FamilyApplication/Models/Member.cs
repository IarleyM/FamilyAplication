using FamilyApplication.Enums;
using System.ComponentModel.DataAnnotations;

namespace FamilyApplication.Models
{
    public class Member
    {
        public long MemberId { get; set; }

        [Required]
        public string MemberName { get; set; }

        public int Age { get; set; }

        public FamilyCategory familyCategory { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string Photo { get; set; }

        [Required]
        public long FamilyId { get; set; }

        public Member() { }
    }
}