using Newtonsoft.Json;
using System;


namespace Domain.Entities
{
    public class Comment: BaseEntity
    {
        public virtual string Text
        { get; set; }

        public virtual int Likes
        { get; set; }

        public virtual int Dislikes
        { get; set; }

        public virtual DateTime Created
        { get; set; }

        public virtual DateTime? Modified
        { get; set; }

        public virtual User User
        { get; set; }

        [JsonIgnore]
        public virtual Post Post
        { get; set; }
    }
}
