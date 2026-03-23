using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAllPostAsync();
        Task<IEnumerable<PostDTO>> GetByMemberIdAsync(long id);
        Task<IEnumerable<PostDTO>> GetByFamilyIdAsync(long id);
        Task<IEnumerable<PostDTO>> GetByFamilyGroupIdAsync(long id);

        Task<IEnumerable<PostFileDTO>> GetPostFileByPostId(long id);

        Task<PostDTO> AddNewPostAsync(PostDTO post);

        Task<PostDTO> DeletePostAsync(long id);
    }
}
