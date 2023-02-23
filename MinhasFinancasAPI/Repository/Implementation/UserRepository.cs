using Dapper;
using MinhasFinancasAPI.Entities;
using MinhasFinancasAPI.Repository.Interface;
using System.Data;
using System.Linq;

namespace MinhasFinancasAPI.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IBaseRepository _baseRepository;

        public UserRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public IEnumerable<User> GetAll()
        {
            try
            {
                var data = _baseRepository.Connection.Query<User>("SELECT * FROM Usuario");

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Save(User user)
        {
            try
            {
                _baseRepository.Connection.Execute("SalvarUsuario", user, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User VerificaUsuario(User user)
        {
            // Implementar function para evitar sql injection
            //todo mudar pra function

            string query = $"select * from Usuario WHERE Email = '{user.Email}' AND Senha ='{user.Senha}'";

            return _baseRepository.Connection.Query<User>(query).FirstOrDefault() ?? new User();


        }

    }
}
