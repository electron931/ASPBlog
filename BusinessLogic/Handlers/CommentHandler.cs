using System.Collections.Generic;
using Domain.Entities;
using Domain.IRepositories;

namespace BusinessLogic.Handlers
{
    public class CommentHandler : BaseHandler
    {
        ICommentRepository rep;

        public CommentHandler() : base() 
        {
            rep = app.getCommentRepository();
        }

        public IList<Comment> Comments()
        {
            return rep.Comments();
        }

        public IList<Comment> CommentsForPost(int postId)
        {
            return rep.CommentsForPost(postId);
        }

        public IList<Comment> UserComments(int userId)
        {
            return rep.UserComments(userId);
        }

        public int TotalComments()
        {
            return rep.TotalComments();
        }

        public int TotalCommentsForPost(int postId)
        {
            return rep.TotalCommentsForPost(postId);
        }

        public int TotalUserComments(int userId)
        {
            return rep.TotalUserComments(userId);
        }

        public Comment Comment(int id)
        {
            return rep.Comment(id);
        }

        public override void Delete(int id)
        {
            rep.Delete(id);
        }
    }
}
