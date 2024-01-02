using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Services.Interfaces.CRUD;

namespace Inventory.Services.CRUD
{
    public class UserService : CRUDService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }

        public new async Task<User?> CreateAsync(User entity)
        {
            User? user = null;
            if (entity.Role is not null && entity.Name != "" && entity.Email != "" && entity.Password != "")
                user = await base.CreateAsync(entity);

            return user;
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _userRepository.ExistsAsync(email);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            return await _userRepository.LoginAsync(email, password);
        }
        
        public async Task<bool> UpdateAsync(int id, string email, string password, User entity)
        {
            bool success = false;

            if (entity.Role is not null && email != "" && password != "")
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

                success = await _userRepository.UpdateAsync(id, email, password, entity);
            }

            return success;
        }
    }
}