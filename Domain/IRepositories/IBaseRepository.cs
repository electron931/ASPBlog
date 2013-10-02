using System.Collections.Generic;
using Domain.Entities;


namespace Domain.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        int Add(T obj);
        void Edit(T obj);
        void Delete(int id);
    }
}
