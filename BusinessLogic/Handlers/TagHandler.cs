using System.Collections.Generic;
using Domain.Entities;
using Domain.IRepositories;

namespace BusinessLogic.Handlers
{
    public class TagHandler : BaseHandler
    {
        ITagRepository rep;

        public TagHandler() : base() 
        {
            rep = app.getTagRepository();
        }

        public IList<Tag> Tags()
        {
            return rep.Tags();
        }

        public int TotalTags()
        {
            return rep.TotalTags();
        }

        public Tag Tag(string tagSlug)
        {
            return rep.Tag(tagSlug);
        }

        public Tag Tag(int id)
        {
            return rep.Tag(id);
        }

        public override void Delete(int id)
        {
            rep.Delete(id);
        }
    }
}
