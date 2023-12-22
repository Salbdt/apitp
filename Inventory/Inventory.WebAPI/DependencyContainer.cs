using Inventory.Persistence.Interfaces;
using Inventory.Persistence.Repositories;
using Inventory.Services;
using Oracle.ManagedDataAccess.Client;

    public static class DependencyContainer
    {
        /*
        public static IServiceCollection AddOracleConnection(
            this IServiceCollection services,
            string connectionString
        )
        {
            services.AddScoped<OracleConnection>(new OracleConnection(connectionString));
            return services;
        }
        */

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<RoleService>();
            services.AddScoped<UserService>();
            
            return services;
        }
    }