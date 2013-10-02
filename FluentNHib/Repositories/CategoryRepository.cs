using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Domain.IRepositories;
using Domain.Entities;


namespace FluentNHib.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ISession session) : base(session) { }

        public IList<Category> Categories()
        {
            return _session.Query<Category>().OrderBy(p => p.Name).ToList();
        }

        public int TotalCategories()
        {
            return _session.Query<Category>().Count();
        }

        public Category Category(string slug)
        {
            return _session.Query<Category>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
        }

        public Category Category(int id)
        {
            return _session.Query<Category>().FirstOrDefault(t => t.Id == id);
        }
    }
}
