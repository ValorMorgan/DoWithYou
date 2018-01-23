using Autofac;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Factories;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Service;
using DoWithYou.Model;
using DoWithYou.Model.Base;
using DoWithYou.Model.Mappers;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants;
using Serilog;

namespace DoWithYou.Service.Utilities
{
    public class ContainerBuilderLayerFactory : IContainerBuilderLayerFactory
    {
        public void RegisterBuilderLayerTypes(ref ContainerBuilder builder)
        {
            RegisterServiceLayerTypes(ref builder);
            RegisterModelLayerTypes(ref builder);
            RegisterDataLayerTypes(ref builder);
        }

        #region PRIVATE
        private static void RegisterDataLayerTypes(ref ContainerBuilder builder)
        {
            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Data Layer Types to {Class}", nameof(ContainerBuilder));
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Data Layer Instances to {Class}", nameof(ContainerBuilder));

                // NOTE: Data Contexts are single instance to reduce EF creation processing.
                var doWithYouContextFactory = new DoWithYouContextFactory();
                build.RegisterInstance<IDoWithYouContext>(doWithYouContextFactory.CreateDbContext(null))
                    .SingleInstance();
            }
        }

        private static void RegisterModelLayerTypes(ref ContainerBuilder builder)
        {
            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Model Layer Types to {Class}", nameof(ContainerBuilder));

                build.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
                build.RegisterType<UserRepository>().As<IRepository<IUser>>().InstancePerLifetimeScope();
                build.RegisterType<UserProfileRepository>().As<IRepository<IUserProfile>>().InstancePerLifetimeScope();
                build.RegisterType<UserModelMapper>().As<IUserModelMapper>();
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Model Layer Instances to {Class}", nameof(ContainerBuilder));
            }
        }

        private static void RegisterServiceLayerTypes(ref ContainerBuilder builder)
        {
            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Service Layer Types to {Class}", nameof(ContainerBuilder));

                build.RegisterGeneric(typeof(DatabaseHandler<>)).As(typeof(IDatabaseHandler<>)).InstancePerLifetimeScope();
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Service Layer Instances to {Class}", nameof(ContainerBuilder));
            }
        }
        #endregion
    }
}