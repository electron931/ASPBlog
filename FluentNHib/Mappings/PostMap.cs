using FluentNHibernate.Mapping;
using Domain.Entities;


namespace FluentNHib.Mappings
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id);

            Map(x => x.Title).Length(500).Not.Nullable();

            Map(x => x.ShortDescription).Length(5000).Not.Nullable();

            Map(x => x.Description).Length(50000).Not.Nullable();

            Map(x => x.Meta).Length(1000).Not.Nullable();

            Map(x => x.UrlSlug).Length(200).Not.Nullable();

            Map(x => x.Image).Length(100).Not.Nullable();

            Map(x => x.Published).Not.Nullable();

            Map(x => x.PostedOn).Not.Nullable();

            Map(x => x.Modified);

            References(x => x.Category).Column("CategoryId").Not.Nullable();

            References(x => x.Author).Column("AuthorId").Not.Nullable();

            HasManyToMany(x => x.Tags).Table("PostTagMap");
        }
    }
}
