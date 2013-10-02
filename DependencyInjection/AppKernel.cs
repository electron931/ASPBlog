using Domain.IRepositories;
using Domain.Entities;
using FluentNHib.Repositories;
using FluentNHib;
using Ninject;


namespace DependencyInjection
{
    public class AppKernel
    {
        private IKernel kernel;

        public AppKernel()
        {
            kernel = new StandardKernel();
            kernel.Load(new RepositoryModule());   
        }

        public IBaseRepository<BaseEntity> getBaseRepository()
        {
            kernel.Bind<IBaseRepository<BaseEntity>>().To<BaseRepository<BaseEntity>>();

            return kernel.Get<IBaseRepository<BaseEntity>>();
        }

        public ICategoryRepository getCategoryRepository()
        {
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();

            return kernel.Get<ICategoryRepository>();
        }

        public ICommentRepository getCommentRepository()
        {
            kernel.Bind<ICommentRepository>().To<CommentRepository>();

            return kernel.Get<ICommentRepository>();
        }

        public IPostRepository getPostRepository()
        {
            kernel.Bind<IPostRepository>().To<PostRepository>();

            return kernel.Get<IPostRepository>();
        }

        public ITagRepository getTagRepository()
        {
            kernel.Bind<ITagRepository>().To<TagRepository>();

            return kernel.Get<ITagRepository>();
        }

        public IUserRepository getUserRepository()
        {
            kernel.Bind<IUserRepository>().To<UserRepository>();

            return kernel.Get<IUserRepository>();
        }

    }
}
