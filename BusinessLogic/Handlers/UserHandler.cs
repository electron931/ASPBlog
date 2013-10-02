using System.Collections.Generic;
using Domain.Entities;
using Domain.IRepositories;

namespace BusinessLogic.Handlers
{
    public class UserHandler : BaseHandler
    {
        IUserRepository rep;

        public UserHandler() : base() 
        {
            rep = app.getUserRepository();
        }

        public IList<User> AllUsers()
        {
            return rep.AllUsers();
        }

        public IList<User> AdminUsers()
        {
            return rep.AdminUsers();
        }

        public IList<User> AuthorUsers()
        {
            return rep.AuthorUsers();
        }

        public IList<User> SubsciberUsers()
        {
            return rep.SubsciberUsers();
        }

        public int TotalUsers()
        {
            return rep.TotalUsers();
        }

        public int TotalAdminUsers()
        {
            return rep.TotalAdminUsers();
        }

        public int TotalAuthorUsers()
        {
            return rep.TotalAuthorUsers();
        }

        public int TotalSubsciberUsers()
        {
            return rep.TotalSubsciberUsers();
        }

        public User User(int id)
        {
            return rep.User(id);
        }

        public override void Delete(int id)
        {
            rep.Delete(id);
        }
    }
}
