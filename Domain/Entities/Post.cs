using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    public class Post: BaseEntity
    {
        public virtual string Title
        { get; set; }

        public virtual string ShortDescription
        { get; set; }

        public virtual string Description
        { get; set; }

        public virtual string Meta
        { get; set; }

        public virtual string UrlSlug
        { get; set; }

        public virtual string Image
        { get; set; }

        public virtual bool Published
        { get; set; }

        public virtual DateTime PostedOn
        { get; set; }

        public virtual DateTime? Modified
        { get; set; }

        public virtual Category Category
        { get; set; }

        public virtual IList<Tag> Tags
        { get; set; }

        public virtual IList<Comment> Comments
        { get; set; }

        public virtual User Author
        { get; set; }
    }
}
