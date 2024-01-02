using Inventory.Persistence;
using Inventory.Persistence.Interfaces;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Persistence.Repositories;
using Inventory.Persistence.Repositories.CRUD;
using Inventory.Services;
using Inventory.Services.CRUD;

    public static class DependencyContainer
    {
        public static IServiceCollection AddDB(this IServiceCollection services)
        {
            services.AddSingleton<OracleDB>();
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
            services.AddScoped<RoleService>();
            services.AddScoped<UserService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<InventoryStockService>();
            services.AddScoped<InventoryMovementService>();

            services.AddScoped<ReportService>();
            
            return services;
        }
    }