using Inventory.Entities;

namespace Inventory.Persistence.Interfaces
{
    public interface IReportRepository
    {
        Task<List<Product>> GetAllProductsBySellerAsync(int userId);
    }
}