using MinhasFinancasAPI.Entities;

namespace MinhasFinancasAPI.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        bool Save(User user);

        User VerificaUsuario(User user);
    }
}

