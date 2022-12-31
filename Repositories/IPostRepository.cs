using trading_app_3_api.Model;

namespace trading_app_3_api.Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>?> GetPosts();
        Task<Post?> GetPost(int postId);
        Task<Post?> PostNewPost(Post postData);
        //Task<Post?> PutPost(Post postData);
        //Task<Post?> PatchPost(Post postData);
        Task<Post?> UpdatePost(Post postData);
        Task<Post?> DeletPost(Post post);
    }
}
