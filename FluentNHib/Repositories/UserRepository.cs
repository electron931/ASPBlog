using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Domain.IRepositories;
using Domain.Entities;

namespace FluentNHib.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ISession session) : base(session) { }

        public IList<User> AllUsers()
        {
            return _session.Query<User>().OrderBy(p => p.UserName).ToList();
        }

        public IList<User> AdminUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Admin).OrderBy(p => p.UserName).ToList();
        }

        public IList<User> AuthorUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Author || p.Type == UserType.Admin).OrderBy(p => p.UserName).ToList();
        }

        public IList<User> SubsciberUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Subscriber).OrderBy(p => p.UserName).ToList();
        }

        public int TotalUsers()
        {
            return _session.Query<User>().Count();
        }

        public int TotalAdminUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Admin).Count();
        }

        public int TotalAuthorUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Author).Count();
        }

        public int TotalSubsciberUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Subscriber).Count();
        }

        public User User(int id)
        {
            return _session.Get<User>(id);
        }

        public User User(string userName)
        {
            var user = _session.Query<User>().Where(p => p.UserName.Equals(userName));
            if (user.Count() > 0)
                return user.First();
            else
                return null;
        }

        public User User(string userName, string password)
        {
            var user = _session.Query<User>().Where(p => p.UserName.Equals(userName) && p.Password.Equals(password));
            if (user.Count() > 0)
                return user.First();
            else
                return null;
        }
    }
}
