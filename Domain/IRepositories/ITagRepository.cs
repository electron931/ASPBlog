using System.Collections.Generic;
using Domain.Entities;


namespace Domain.IRepositories
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        IList<Tag> Tags();
        int TotalTags();
        Tag Tag(string tagSlug);
        Tag Tag(int id);
    }
}
