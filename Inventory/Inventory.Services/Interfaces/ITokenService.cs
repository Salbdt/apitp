using Inventory.Entities;

namespace Inventory.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}