using Inventory.Persistence;
using Inventory.Persistence.Interfaces;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Persistence.Repositories;
using Inventory.Persistence.Repositories.CRUD;
using Inventory.Services;
using Inventory.Services.CRUD;
using Inventory.Services.Interfaces;
using Inventory.Services.Interfaces.CRUD;

public static class DependencyContainer
    {
        public static IServiceCollection AddOracle(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            services.AddSingleton(new OracleDB(configuration.GetConnectionString(connectionString)));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // CRUD
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryStockRepository, InventoryStockRepository>();
            services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
            
            services.AddScoped<IReportRepository, ReportRepository>();
            
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // CRUD
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryStockService, InventoryStockService>();
            services.AddScoped<IInventoryMovementService, InventoryMovementService>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ITokenService, TokenService>();
            
            return services;
        }
    }