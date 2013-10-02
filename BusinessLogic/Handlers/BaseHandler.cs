using DependencyInjection;
using Domain.Entities;
using Domain.IRepositories;

namespace BusinessLogic.Handlers
{
    public class BaseHandler
    {
        private IBaseRepository<BaseEntity> rep;
        protected AppKernel app;

        public BaseHandler()
        {
            app = new AppKernel();
            rep = app.getBaseRepository();
        }

        public int Add(BaseEntity obj)
        {
            return rep.Add(obj);
        }

        public void Edit(BaseEntity obj)
        {
            rep.Edit(obj);
        }

        public virtual void Delete(int id)
        {
            rep.Delete(id);
        }
    }
}
