using System.Collections.Generic;
using Domain.Entities;


namespace Domain.IRepositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        IList<Comment> Comments();
        IList<Comment> CommentsForPost(int postId);
        IList<Comment> UserComments(int userId);

        int TotalComments();
        int TotalCommentsForPost(int postId);
        int TotalUserComments(int userId);
        Comment Comment(int id);
    }
}
