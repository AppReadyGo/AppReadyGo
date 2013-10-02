using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using AppReadyGo.Core;
using AppReadyGo.Core.Queries;
using AppReadyGo.Domain.Queries;
using AppReadyGo.Domain.CommandHandlers;
using System.Reflection;
using AppReadyGo.Core.Commands;
using AppReadyGo.Domain;
using NHibernate;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System.Configuration;
using NHibernate.Mapping.ByCode;
using NHibernate.Driver;
using NHibernate.Dialect;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Web;

namespace AppReadyGo.Common
{
    public class ObjectContainer : IObjectContainer
    {
        private static readonly object locker = new object();
        private static IObjectContainer instance = null;

        private readonly WindsorContainer container = new WindsorContainer();
        private IRepository repository;

        private NHibernateHelper nhibernateHelper;
        //private NHibernate.Cfg.Configuration configuration;

        public static IObjectContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ObjectContainer();
                        }
                    }
                }
                return instance;
            }
        }

        public CurrentUserDetails CurrentUserDetails
        {
            get
            {
                var securityContext = container.Resolve<ISecurityContext>();
                return securityContext.CurrentUser;
            }
        }

        private ObjectContainer()
        {
            var dbSettings = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
            
            this.nhibernateHelper = new NHibernateHelper(ConfigurationManager.ConnectionStrings[dbSettings.DefaultDatabase].ToString(), SchemaAutoAction.Validate);

            bool isWebApp = HttpContext.Current != null;

            var parentCommandHandler = typeof(ICommandHandler<,>);
            foreach (var type in parentCommandHandler.Assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    var @interface = type.GetInterfaces().FirstOrDefault();
                    if (@interface != null && @interface.IsGenericType && @interface.GetGenericTypeDefinition() == parentCommandHandler)
                    {
                        if (isWebApp)
                        {
                            container.Register(Component.For(@interface).ImplementedBy(type).LifeStyle.PerWebRequest);
                        }
                        else
                        {
                            container.Register(Component.For(@interface).ImplementedBy(type).LifeStyle.PerThread);
                        }
                    }
                }
            }

            var parentQueryHandler = typeof(IQueryHandler<,>);
            foreach (var type in parentQueryHandler.Assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    var @interface = type.GetInterfaces().FirstOrDefault();
                    if (@interface != null && @interface.IsGenericType && @interface.GetGenericTypeDefinition() == parentQueryHandler)
                    {
                        if (isWebApp)
                        {
                            container.Register(Component.For(@interface).ImplementedBy(type).LifeStyle.PerWebRequest);
                        }
                        else
                        {
                            container.Register(Component.For(@interface).ImplementedBy(type).LifeStyle.PerThread);
                        }
                    }
                }
            }
            container.Register(Component.For<IObjectContainer>().Instance(this));
            container.Register(Component.For<IRepository>().ImplementedBy<Repository>());
            if (isWebApp)
            {
                container.Register(Component.For<ISecurityContext>().ImplementedBy<SecurityContext>().LifeStyle.PerWebRequest);
                container.Register(Component.For<IValidationContext>().ImplementedBy<ValidationContext>().LifeStyle.PerWebRequest);
            }
            else
            {
                container.Register(Component.For<ISecurityContext>().ImplementedBy<SecurityContext>().LifeStyle.PerThread);
                container.Register(Component.For<IValidationContext>().ImplementedBy<ValidationContext>().LifeStyle.PerThread);
            }

            this.repository = container.Resolve<IRepository>();
        }

        public void ClearCurrentUserDetails()
        {
            container.Resolve<ISecurityContext>().ClearCurrentUserDetails();
        }

        private ICommandHandler<TCommand, TResult> GetCommandHandler<TCommand, TResult>(ICommand<TResult> command)
        {
            return container.Resolve<ICommandHandler<TCommand, TResult>>();
        }

        private ISession OpenSession()
        {
            return nhibernateHelper.OpenSession();
        }

        public TResult RunQuery<TResult>(IQuery<TResult> query)
        {
            Type handlerTypeBluprint = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(TResult) };
            var obj = container.Resolve(handlerTypeBluprint.MakeGenericType(typeArgs));

            MethodInfo method = obj.GetType().GetMethod("Run");

            using (ISession session = OpenSession())
            {

                return (TResult)method.Invoke(obj, new object[] { session, query });
            }
        }

        public CommandResult<TResult> Dispatch<TResult>(ICommand<TResult> command)
        {
            Type handlerTypeBluprint = typeof(ICommandHandler<,>);
            Type[] typeArgs = { command.GetType(), typeof(TResult) };
            var obj = container.Resolve(handlerTypeBluprint.MakeGenericType(typeArgs));
            MethodInfo method = obj.GetType().GetMethod("Execute");
            var commandResult = new CommandResult<TResult>();
            using (ISession session = nhibernateHelper.OpenSession())
            {
                using (ITransaction dbTrans = session.BeginTransaction())
                {
                    var list = new List<ValidationResult>();
                    list.AddRange(command.ValidatePermissions(container.Resolve<ISecurityContext>()));
                    list.AddRange(command.Validate(new ValidationContext(session, container.Resolve<ISecurityContext>())));
                    commandResult.Validation = list;
                    if (!list.Any())
                    {
                        commandResult.Result = (TResult)method.Invoke(obj, new object[] { session, command });
                        dbTrans.Commit();
                    }
                    else
                    {
                        dbTrans.Rollback();
                    }
                }
                return commandResult;
            }
            //TODO: Events
        }
    }
}
