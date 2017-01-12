using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace SpecificationPatternExample
{
    public static class HibernateHelper
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return
                Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008
                        .ConnectionString(c => c
                            .FromConnectionStringWithKey("connection_info"))
                     )
                    .Mappings(m => m
                        .FluentMappings.AddFromAssemblyOf<Program>())
                    .BuildSessionFactory();
        }
    }
}
