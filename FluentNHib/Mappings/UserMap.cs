using FluentNHibernate.Mapping;
using Domain.Entities;


namespace FluentNHib.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.Login).Length(50).Not.Nullable();

            Map(x => x.Password).Length(50).Not.Nullable();

            Map(x => x.Type).Length(20).Not.Nullable();
        }
    }
}
