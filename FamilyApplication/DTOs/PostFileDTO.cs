using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.DTOs
{
    public class PostFileDTO
    {
        public Guid PostFileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long PostId { get; set; }
        public string ContentType { get; set; }
    }
    public class CreateFilePostDTO
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long PostId { get; set; }
        public string contentType { get; set; }
    }
}
