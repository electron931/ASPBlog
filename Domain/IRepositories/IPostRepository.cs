using System.Collections.Generic;
using Domain.Entities;


namespace Domain.IRepositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        IList<Post> Posts();
        IList<Post> PostsForTag(string tagSlug);
        IList<Post> PostsForCategory(string categorySlug);
        IList<Post> PostsForSearch(string search);
        IList<Post> UserPosts(int userId);

        int TotalPosts(bool checkIsPublished = true);
        int TotalPostsForCategory(string categorySlug);
        int TotalPostsForTag(string tagSlug);
        int TotalPostsForSearch(string search);
        int TotalUserPosts(int userId);

        IList<Post> Posts(string sortColumn, bool sortByAscending);
        Post Post(int year, int month, string titleSlug);
        Post Post(int id);
    }
}
