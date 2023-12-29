using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Services.Interfaces.CRUD;

namespace Inventory.Services.CRUD
{
    public class RoleService : CRUDService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository repository) : base(repository)
        {
            _roleRepository = repository;
        }
    }
}