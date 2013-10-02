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
            return _session.Query<User>().OrderBy(p => p.Login).ToList();
        }

        public IList<User> AdminUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Admin).OrderBy(p => p.Login).ToList();
        }

        public IList<User> AuthorUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Author).OrderBy(p => p.Login).ToList();
        }

        public IList<User> SubsciberUsers()
        {
            return _session.Query<User>().Where(p => p.Type == UserType.Subscriber).OrderBy(p => p.Login).ToList();
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
    }
}
