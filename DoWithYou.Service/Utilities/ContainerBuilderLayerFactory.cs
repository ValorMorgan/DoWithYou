using Autofac;
using DoWithYou.Data.Contexts;
using DoWithYou.Interface.Data;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Service;
using DoWithYou.Model.Repository;

namespace DoWithYou.Service.Utilities
{
    public class ContainerBuilderLayerFactory : IContainerBuilderLayerFactory
    {
        public void RegisterBuilderLayerTypes(ref ContainerBuilder builder)
        {
            // Service
            builder.RegisterType<DatabaseHandler<IUser>>().As<IDatabaseHandler<IUser>>();
            builder.RegisterType<DatabaseHandler<IUserProfile>>().As<IDatabaseHandler<IUserProfile>>();

            // Model
            builder.RegisterType<UserRepository>().As<IRepository<IUser>>();
            builder.RegisterType<UserProfileRepository>().As<IRepository<IUserProfile>>();

            // Data
            builder.RegisterType<DoWithYouContext>().As<IDoWithYouContext>();
        }
    }
}