using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Domain.IRepositories;
using Domain.Entities;

namespace FluentNHib.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ISession session) : base(session) { }

        public IList<Comment> Comments()
        {
            return _session.Query<Comment>().OrderBy(p => p.Created).ToList();
        }

        public IList<Comment> CommentsForPost(int postId)
        {
            return _session.Query<Comment>().Where(x => x.Post.Id == postId).ToList();
        }

        public IList<Comment> UserComments(int userId)
        {
            return _session.Query<Comment>().Where(x => x.User.Id == userId).ToList();
        }

        public int TotalComments()
        {
            return _session.Query<Comment>().Count();
        }

        public int TotalCommentsForPost(int postId)
        {
            return _session.Query<Comment>().Where(x => x.Post.Id == postId).Count();
        }

        public int TotalUserComments(int userId)
        {
            return _session.Query<Comment>().Where(x => x.User.Id == userId).Count();
        }

        public Comment Comment(int id)
        {
            return _session.Get<Comment>(id);
        }
    }
}
