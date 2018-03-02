using Autofac;
using DoWithYou.Data.Contexts;
using DoWithYou.Data.Factories;
using DoWithYou.Data.Mappers;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;
using DoWithYou.Model;
using DoWithYou.Model.Base;
using DoWithYou.Model.Mappers;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace DoWithYou.Service.Utilities
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterLayerTypes(this ContainerBuilder builder, IConfiguration config = null)
        {
            RegisterServiceLayerTypes(ref builder);
            RegisterModelLayerTypes(ref builder);
            RegisterDataLayerTypes(ref builder, config);
            return builder;
        }

        #region PRIVATE
        private static void RegisterDataLayerTypes(ref ContainerBuilder builder, IConfiguration config)
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

                // NOTE: Context are retrieved through the "Mapper"
                // Issues with <out T> type on the factory and with "RegisterInstance" when we want the context to live per request / scope

                build.RegisterGeneric(typeof(EntityDatabaseMapper<>)).As(typeof(IEntityDatabaseMapper<>)).SingleInstance();
            }
        }

        private static void RegisterModelLayerTypes(ref ContainerBuilder builder)
        {
            RegisterTypes(ref builder);
            RegisterInstances(ref builder);

            void RegisterTypes(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Model Layer Types to {Class}", nameof(ContainerBuilder));

                build.RegisterType<UserModelMapper>().As<IModelMapper<IUserModel, IUser, IUserProfile>>().SingleInstance();

                build.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
                build.RegisterType<UserRepository>().As<IRepository<IUser>>().InstancePerLifetimeScope();
                build.RegisterType<UserProfileRepository>().As<IRepository<IUserProfile>>().InstancePerLifetimeScope();
                build.RegisterType<UserModelRepository>().As<IModelRepository<IUserModel, IUser, IUserProfile>>().InstancePerLifetimeScope();
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
                
                build.RegisterGeneric(typeof(ModelHandler<,>)).As(typeof(IModelHandler<,>)).InstancePerLifetimeScope();
                build.RegisterGeneric(typeof(ModelHandler<,,>)).As(typeof(IModelHandler<,,>)).InstancePerLifetimeScope();
            }

            void RegisterInstances(ref ContainerBuilder build)
            {
                Log.Logger.LogEventDebug(LoggerEvents.STARTUP, "Registering Service Layer Instances to {Class}", nameof(ContainerBuilder));
            }
        }
        #endregion
    }
}