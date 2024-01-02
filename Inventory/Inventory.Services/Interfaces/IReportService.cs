using Inventory.Entities;

namespace Inventory.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<Product>> GetAllProductsBySellerAsync(int userId);
    }
}