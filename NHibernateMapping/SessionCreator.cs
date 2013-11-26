using NHibernate;
using NHibernate.Cfg;

namespace NHibernateMapping
{
    public class SessionCreator
    {
        public ISessionFactory SessionFactory= (new Configuration()).Configure().BuildSessionFactory();

        public ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
