using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Domain.IRepositories;
using Domain.Entities;

namespace FluentNHib.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ISession session) : base(session) { }

        public IList<Post> Posts()
        {
            var query = _session.Query<Post>()
                                //.Where(p => p.Published)
                                .OrderByDescending(p => p.PostedOn)
                                .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public IList<Post> PostsForPage(int pageNumber, int pageLimit)
        {
            var query = _session.Query<Post>()
                            .Where(p => p.Published)
                            .OrderByDescending(p => p.PostedOn)
                            .Skip(pageNumber * pageLimit)
                            .Take(pageLimit)
                            .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNumber, int pageLimit)
        {
            var query = _session.Query<Post>()
                                .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                                .OrderByDescending(p => p.PostedOn)
                                .Skip(pageNumber * pageLimit)
                                .Take(pageLimit)
                                .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNumber, int pageLimit)
        {
            var query = _session.Query<Post>()
                               .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                               .OrderByDescending(p => p.PostedOn)
                               .Skip(pageNumber * pageLimit)
                               .Take(pageLimit)
                               .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public IList<Post> PostsForSearch(string search, int pageNumber, int pageLimit)
        {
            var query = _session.Query<Post>()
                           .Where(p => p.Published && (p.Title.Contains(search) ||
                               p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                           .OrderByDescending(p => p.PostedOn)
                           .Skip(pageNumber * pageLimit)
                           .Take(pageLimit)
                           .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public IList<Post> UserPosts(int userId, int pageNumber, int pageSize, string sortColumn, bool sortByAscending)
        {
            IQueryable<Post> query;

            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderBy(p => p.Title)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderByDescending(p => p.Title)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "Published":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderBy(p => p.Published)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderByDescending(p => p.Published)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "PostedOn":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderBy(p => p.PostedOn)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderByDescending(p => p.PostedOn)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "Modified":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderBy(p => p.Modified)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderByDescending(p => p.Modified)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "Category":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderBy(p => p.Category.Name)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .Where(p => p.Author.Id == userId)
                                 .OrderByDescending(p => p.Category.Name)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                default:
                    query = _session.Query<Post>()
                             .Where(p => p.Author.Id == userId)
                             .OrderByDescending(p => p.PostedOn)
                             .Skip(pageNumber * pageSize)
                             .Take(pageSize)
                             .Fetch(p => p.Category);
                    break;
            }

            if (query == null)
                return null;

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return _session.Query<Post>().Where(p => checkIsPublished || p.Published == true).Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _session.Query<Post>()
                          .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                          .Count();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _session.Query<Post>()
                           .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                           .Count();
        }

        public int TotalPostsForSearch(string search)
        {
            return _session.Query<Post>()
                           .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search)
                               || p.Tags.Any(t => t.Name.Equals(search))))
                           .Count();
        }

        public int TotalUserPosts(int userId, bool checkIsPublished = true)
        {
            return _session.Query<Post>().Where(p => p.Author.Id == userId).
                                          Where(p => checkIsPublished || p.Published == true).Count();
        }

        public IList<Post> Posts(int pageNumber, int pageSize, string sortColumn, bool sortByAscending)
        {
            IQueryable<Post> query;

            switch (sortColumn)
            {
                case "Title":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .OrderBy(p => p.Title)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .OrderByDescending(p => p.Title)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "Published":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .OrderBy(p => p.Published)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .OrderByDescending(p => p.Published)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "PostedOn":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .OrderBy(p => p.PostedOn)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .OrderByDescending(p => p.PostedOn)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "Modified":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .OrderBy(p => p.Modified)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .OrderByDescending(p => p.Modified)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                case "Category":
                    if (sortByAscending)
                        query = _session.Query<Post>()
                                 .OrderBy(p => p.Category.Name)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    else
                        query = _session.Query<Post>()
                                 .OrderByDescending(p => p.Category.Name)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .Fetch(p => p.Category);
                    break;
                default:
                    query = _session.Query<Post>()
                             .OrderByDescending(p => p.PostedOn)
                             .Skip(pageNumber * pageSize)
                             .Take(pageSize)
                             .Fetch(p => p.Category);
                    break;
            }

            if (query == null)
                return null;

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().ToList();
        }

        public Post Post(int year, int month, string titleSlug)
        {
            var query = _session.Query<Post>()
                           .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                           .Fetch(p => p.Category);

            if (query == null)
                return null;

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().Single();
        }

        public Post Post(string categorySlug, string titleSlug)
        {
            var query = _session.Query<Post>()
                           .Where(p => p.Category.UrlSlug == categorySlug && p.UrlSlug.Equals(titleSlug));

            if (query == null)
                return null;

            query.FetchMany(p => p.Tags).ToFuture();


            return query.ToFuture().Single();
        }

        public Post Post(int id)
        {
            return _session.Get<Post>(id);
        }
    }
}
