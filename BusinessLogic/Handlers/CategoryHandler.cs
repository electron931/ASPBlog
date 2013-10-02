using System.Collections.Generic;
using Domain.Entities;
using Domain.IRepositories;

namespace BusinessLogic.Handlers
{
    public class CategoryHandler : BaseHandler
    {
        ICategoryRepository rep;

        public CategoryHandler() : base() 
        {
            rep = app.getCategoryRepository();
        }

        public IList<Category> Categories()
        {
            return rep.Categories();
        }

        public int TotalCategories()
        {
            return rep.TotalCategories();
        }

        public Category Category(string categorySlug)
        {
            return rep.Category(categorySlug);
        }

        public Category Category(int id)
        {
            return rep.Category(id);
        }

        public override void Delete(int id)
        {
            rep.Delete(id);
        }
    }
}
