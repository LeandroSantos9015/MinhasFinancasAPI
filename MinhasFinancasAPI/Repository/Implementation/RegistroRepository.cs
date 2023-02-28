using Dapper;
using MinhasFinancasAPI.Entities;
using MinhasFinancasAPI.Repository.Interface;
using System.Data;
using System.Linq;

namespace MinhasFinancasAPI.Repository.Implementation
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly IBaseRepository _baseRepository;

        public RegistroRepository(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public IList<Registro> Balanco(long idUsuario)
        {

            string sql = "select * from registro where idusuario = " + idUsuario;

            return _baseRepository.Connection.Query<Registro>(sql).ToList();
        }

        public bool SaveRegister(Registro registro)
        {
            try
            {
                _baseRepository.Connection.Execute("SalvarRegistro", registro, commandType: CommandType.StoredProcedure);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Registro> RetornarDiario(string data, int idUsuario)
        {
            string sql = $"select * from registro where idusuario = 2 and date between '{data}T00:00:00' and '{data}T23:59:59' and idUsuario ={idUsuario}";

            return _baseRepository.Connection.Query<Registro>(sql).ToList();
        }

        public bool ApagarRegistro(int id)
        {
            string sql = $"delete registro where id = {id}";

            _baseRepository.Connection.Execute(sql);

            return true;
        }

    }
}
