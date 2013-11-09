using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Domain.IRepositories;
using Domain.Entities;
using System;

namespace FluentNHib.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(ISession session) : base(session) { }

        public IList<Tag> Tags()
        {
            return _session.Query<Tag>().OrderBy(p => p.Name).ToList();
        }

        public int TotalTags()
        {
            return _session.Query<Tag>().Count();
        }

        public Tag Tag(string slug)
        {
            return _session.Query<Tag>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
        }

        public Tag Tag(int id)
        {
            return _session.Query<Tag>().FirstOrDefault(t => t.Id == id);
        }
    }
}
