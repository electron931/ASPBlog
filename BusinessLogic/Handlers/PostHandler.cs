using System.Collections.Generic;
using Domain.Entities;
using Domain.IRepositories;

namespace BusinessLogic.Handlers
{
    public class PostHandler : BaseHandler
    {
        IPostRepository rep;

        public PostHandler() : base() 
        {
            rep = app.getPostRepository();
        }

        public IList<Post> Posts()
        {
            return rep.Posts();
        }

        public IList<Post> PostsForPage(int pageNumber, int pageLimit)
        {
            return rep.PostsForPage(pageNumber, pageLimit);
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageLimit)
        {
            return rep.PostsForTag(tagSlug, pageNumber, pageLimit);
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageLimit)
        {
            return rep.PostsForCategory(categorySlug, pageNumber, pageLimit);
        }

        public IList<Post> PostsForSearch(string search, int pageNumber, int pageLimit)
        {
            return rep.PostsForSearch(search, pageNumber, pageLimit);
        }

        public IList<Post> UserPosts(int userId, int pageNumber, int pageSize, string sortColumn, bool sortByAscending)
        {
            return rep.UserPosts(userId, pageNumber, pageSize, sortColumn, sortByAscending);
        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return rep.TotalPosts(checkIsPublished);
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return rep.TotalPostsForCategory(categorySlug);
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return rep.TotalPostsForTag(tagSlug);
        }

        public int TotalPostsForSearch(string search)
        {
            return rep.TotalPostsForSearch(search);
        }

        public int TotalUserPosts(int userId, bool checkIsPublished = true)
        {
            return rep.TotalUserPosts(userId, checkIsPublished);
        }

        public IList<Post> Posts(int pageNumber, int pageSize, string sortColumn, bool sortByAscending)
        {
            return rep.Posts(pageNumber, pageSize, sortColumn, sortByAscending);
        }

        public Post Post(int year, int month, string titleSlug)
        {
            return rep.Post(year, month, titleSlug);
        }

        public Post Post(string categorySlug, string postSlug)
        {
            return rep.Post(categorySlug, postSlug);
        }

        public Post Post(int id)
        {
            return rep.Post(id);
        }

        public override void Delete(int id)
        {
            rep.Delete(id);
        }
    }
}
