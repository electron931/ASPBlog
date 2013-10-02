using NHibernate;
using Ninject.Modules;


namespace FluentNHib
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            NHibernateHelper nHelper = new NHibernateHelper();

            Bind<ISession>()
                .ToMethod((ctx) => nHelper.OpenSession());
        }
    }
}
