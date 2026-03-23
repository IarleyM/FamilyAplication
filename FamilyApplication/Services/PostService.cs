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

        public async Task<PostDTO> AddNewPostAsync(PostDTO postdto)
        {
            Post post = new Post
            {
                Title = postdto.Title,
                Description = postdto.Description,
                MemberId = postdto.MemberId,
                CreationDate = DateTime.Now,
                Files = postdto.Files?.Select(f => new PostFile
                {
                    PostFileId = f.PostFileId,
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    ContentType = f.ContentType
                }).ToList()
            };

            var created = await _repository.AddNewPostAsync(post);
            return MapToPostDto(created);
        }

        public Task<PostDTO> DeletePostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostAsync()
        {
            var posts = await _repository.GetAllPostAsync();
            return posts.Select(m => MapToPostDto(m));
        }

        public Task<IEnumerable<PostDTO>> GetByFamilyGroupIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostDTO>> GetByFamilyIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostDTO>> GetByMemberIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostFileDTO>> GetPostFileByPostId(long id)
        {
            var posts = await _repository.GetPostFileByPostId(id);
            return MapToPostFileDto(posts);
        }

        private PostDTO MapToPostDto(Post post)
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
                    ContentType = f.ContentType
                }).ToList()
            };
        }

        private List<PostFileDTO> MapToPostFileDto(IEnumerable<PostFile> post)
        {
            if (post == null)
                return new List<PostFileDTO>();

            return post.Select(postFile => new PostFileDTO
            {
                PostFileId = postFile.PostFileId,
                FileName = postFile.FileName,
                FilePath = postFile.FilePath,
                PostId = postFile.PostId,
                ContentType = postFile.ContentType
            }).ToList();
        }
    }
}
