using FluentNHibernate.Mapping;
using Domain.Entities;


namespace FluentNHib.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.UserName).Length(50).Not.Nullable();

            Map(x => x.Password).Length(50).Not.Nullable();

            Map(x => x.Type).Length(20).Not.Nullable();

            Map(x => x.FirstName).Length(50);

            Map(x => x.LastName).Length(50);

            Map(x => x.Email).Length(50);

            Map(x => x.Info).Length(1000);
        }
    }
}
