using System.Collections.Generic;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository : IBaseRepository<User> 
    {
        IList<User> AllUsers();
        IList<User> AdminUsers();
        IList<User> AuthorUsers();
        IList<User> SubsciberUsers();

        int TotalUsers();
        int TotalAdminUsers();
        int TotalAuthorUsers();
        int TotalSubsciberUsers();
        User User(int id);
        User User(string userName);
        User User(string userName, string password);
    }
}
