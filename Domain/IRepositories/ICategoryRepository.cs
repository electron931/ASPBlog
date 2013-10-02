using System.Collections.Generic;
using Domain.Entities;


namespace Domain.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        IList<Category> Categories();
        int TotalCategories();
        Category Category(string categorySlug);
        Category Category(int id);
    }
}
