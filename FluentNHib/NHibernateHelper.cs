using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using FluentNHib.Mappings;
using NHibernate.Tool.hbm2ddl;
using System;
using NLog;
using NHibernate.Dialect;


namespace FluentNHib
{
    public class NHibernateHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ISessionFactory _sessionFactory;

        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();

                return _sessionFactory;
            }
        }

        private void InitializeSessionFactory()
        {
            try
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(c => c
                                     .Server("localhost")
                                     .Database("aspblog_db")
                                     .Username("root")
                                     .Password(""))
                                  .Dialect<MySQL5Dialect>()
                                  .ShowSql()
                    )
                    .Mappings(m =>
                              m.FluentMappings
                                  .AddFromAssemblyOf<PostMap>())
                    //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                    .BuildSessionFactory();
            }
            catch (Exception e)
            {
                logger.Fatal(e.Message + ": " + e.InnerException.Message);
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

    }
}
