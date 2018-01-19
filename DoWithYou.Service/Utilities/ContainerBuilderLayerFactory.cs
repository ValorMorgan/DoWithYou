using Autofac;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Factories;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Service;
using DoWithYou.Model;
using DoWithYou.Model.Base;
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
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Data Layer to {Class}", nameof(ContainerBuilder));

            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Data Layer Types to {Class}", nameof(ContainerBuilder));
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Data Layer Instances to {Class}", nameof(ContainerBuilder));

                var doWithYouContextFactory = new DoWithYouContextFactory();
                build.RegisterInstance<IDoWithYouContext>(doWithYouContextFactory.CreateDbContext(null));
            }
        }

        private static void RegisterModelLayerTypes(ref ContainerBuilder builder)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Model Layer to {Class}", nameof(ContainerBuilder));

            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Model Layer Types to {Class}", nameof(ContainerBuilder));

                build.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
                build.RegisterType<UserRepository>().As<IRepository<IUser>>();
                build.RegisterType<UserProfileRepository>().As<IRepository<IUserProfile>>();
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Model Layer Instances to {Class}", nameof(ContainerBuilder));
            }
        }

        private static void RegisterServiceLayerTypes(ref ContainerBuilder builder)
        {
            Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Service Layer to {Class}", nameof(ContainerBuilder));

            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Service Layer Types to {Class}", nameof(ContainerBuilder));

                build.RegisterGeneric(typeof(DatabaseHandler<>)).As(typeof(IDatabaseHandler<>));
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Service Layer Instances to {Class}", nameof(ContainerBuilder));
            }
        }
        #endregion
    }
}