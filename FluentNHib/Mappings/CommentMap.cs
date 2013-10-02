using FluentNHibernate.Mapping;
using Domain.Entities;


namespace FluentNHib.Mappings
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id);

            Map(x => x.Text).Length(100).Not.Nullable();

            Map(x => x.Likes);

            Map(x => x.Dislikes);

            Map(x => x.Created).Not.Nullable();

            Map(x => x.Modified);

            References(x => x.User).Column("UserId").Not.Nullable();

            References(x => x.Post).Column("PostId").Not.Nullable();
        }
    }
}
