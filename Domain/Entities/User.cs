
namespace Domain.Entities
{
    public class User: BaseEntity
    {
        public virtual string Login
        { get; set; }

        public virtual string Password
        { get; set; }

        public virtual UserType Type
        { get; set; }
    }
}
