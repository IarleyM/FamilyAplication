using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task<IEnumerable<Post>> GetByMemberIdAsync(long id);
        Task<IEnumerable<Post>> GetByFamilyIdAsync(long id);
        Task<IEnumerable<Post>> GetByFamilyGroupIdAsync(long id);

        Task<IEnumerable<PostFile>> GetPostFileByPostId(long id);

        Task<Post> AddNewPostAsync(Post post);

        Task<Post> DeletePostAsync(long id);
    }
}
