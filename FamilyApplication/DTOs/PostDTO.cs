using FamilyApplication.Models;

namespace FamilyApplication.DTOs
{
    public class PostDTO
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long MemberId { get; set; }
        public List<PostFileDTO> Files { get; set; }
    }
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long MemberId { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
