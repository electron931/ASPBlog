using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Entities
{
    public class User: BaseEntity
    {
        public virtual string UserName
        { get; set; }

        public virtual string Password
        { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual UserType Type
        { get; set; }

        public virtual string FirstName
        { get; set; }

        public virtual string LastName
        { get; set; }

        public virtual string Email
        { get; set; }

        public virtual string Info
        { get; set; }
    }
}
