using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository repository)
        {
            _reportRepository = repository;
        }

        public async Task<List<Product>> GetAllProductsBySellerAsync(int userId)
        {
            var items = await _reportRepository.GetAllProductsBySellerAsync(userId);

            return items;
        }
    }
}