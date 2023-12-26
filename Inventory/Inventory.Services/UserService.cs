using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }
        
        public async Task<bool> UpdateAsync(int id, string email, string password, User entity)
        {
            /*
                Los valores dentro de la entidad se actualizan.
                Si el usuario no quiere modificar la clave y la deja vacía, esa sería su nueva clave.
                Para eso verificamos y la completamos con la clave necesaria para actualizar.
                Lo mismo pasa con el email.
            */
            if (entity.Email == "")
                entity.Email = email;

            if (entity.Password == "")
                entity.Password = password;

            var item = await _userRepository.UpdateAsync(id, email, password, entity);

            return item;
        }
    }
}