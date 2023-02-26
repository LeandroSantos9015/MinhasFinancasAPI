using MinhasFinancasAPI.Entities;

namespace MinhasFinancasAPI.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        bool Save(User user);

        bool SaveRegister(Registro registro);

        User VerificaUsuario(User user);
    }
}

