using System.Collections.Generic;
using Domain.Entities;


namespace Domain.IRepositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        IList<Post> Posts();
        IList<Post> PostsForPage(int pageNumber, int pageLimit);
        IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageLimit);
        IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageLimit);
        IList<Post> PostsForSearch(string search, int pageNumber, int pageLimit);
        IList<Post> UserPosts(int userId, int pageNumber, int pageSize, string sortColumn, bool sortByAscending);

        int TotalPosts(bool checkIsPublished = true);
        int TotalPostsForCategory(string categorySlug);
        int TotalPostsForTag(string tagSlug);
        int TotalPostsForSearch(string search);
        int TotalUserPosts(int userId, bool checkIsPublished = true);

        IList<Post> Posts(int pageNumber, int pageSize, string sortColumn, bool sortByAscending);
        Post Post(int year, int month, string titleSlug);
        Post Post(string categorySlug, string postSlug);
        Post Post(int id);
    }
}
