using FamilyApplication.DTOs;
using FamilyApplication.Models;
using FamilyApplication.Repositories;

namespace FamilyApplication.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<Post> AddNewPostAsync(Post post)
        {
            var created = await _repository.AddNewPostAsync(post);
            return created;
        }

        public Task<Post> DeletePostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            var posts = await _repository.GetAllPostAsync();
            return posts;
        }

        public Task<IEnumerable<Post>> GetByFamilyGroupIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByFamilyIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByMemberIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        private PostDTO MapToDto(Post post)
        {
            return new PostDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                MemberId = post.MemberId,
                Files = post.Files?.Select(f => new PostFileDTO
                {
                    PostFileId = f.PostFileId,
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    PostId = f.PostId,
                }).ToList()
            };
        }
    }
}
