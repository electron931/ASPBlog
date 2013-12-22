using Newtonsoft.Json;
using System.Collections.Generic;


namespace Domain.Entities
{
    public class Tag: BaseEntity
    {
        public virtual string Name
        { get; set; }

        public virtual string UrlSlug
        { get; set; }

        public virtual string Description
        { get; set; }

        [JsonIgnore]
        public virtual IList<Post> Posts
        { get; set; }
    }
}
