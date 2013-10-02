using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Linq;
using Domain.IRepositories;
using Domain.Entities;
using NLog;


namespace FluentNHib.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        // NHibernate object
        protected readonly ISession _session;

        public BaseRepository(ISession session)
        {
            _session = session;
        }

        public int Add(T obj)
        {
            try
            {
                using (var tran = _session.BeginTransaction())
                {
                    _session.Save(obj);
                    tran.Commit();
                    return obj.Id;
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        public void Edit(T obj)
        {
            try
            {
                using (var tran = _session.BeginTransaction())
                {
                    _session.SaveOrUpdate(obj);
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var tran = _session.BeginTransaction())
                {
                    var obj = _session.Get<T>(id);
                    if (obj != null) _session.Delete(obj);
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
