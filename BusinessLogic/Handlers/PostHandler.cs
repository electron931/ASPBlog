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

        public IList<Post> PostsForPage(int pageNumber, int pageSize)
        {
            return rep.PostsForPage(pageNumber, pageSize);
        }

        public IList<Post> PostsForTag(string tagSlug)
        {
            return rep.PostsForTag(tagSlug);
        }

        public IList<Post> PostsForCategory(string categorySlug)
        {
            return rep.PostsForCategory(categorySlug);
        }

        public IList<Post> PostsForSearch(string search)
        {
            return rep.PostsForSearch(search);
        }

        public IList<Post> UserPosts(int userId)
        {
            return rep.UserPosts(userId);
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

        public int TotalUserPosts(int userId)
        {
            return rep.TotalUserPosts(userId);
        }

        public IList<Post> Posts(string sortColumn, bool sortByAscending)
        {
            return rep.Posts(sortColumn, sortByAscending);
        }

        public Post Post(int year, int month, string titleSlug)
        {
            return rep.Post(year, month, titleSlug);
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
