using MinhasFinancasAPI.Repository.Interface;
using MinhasFinancasAPI.Util;
using MinhasFinancasAPI.Util.Enumerator;
using System.Data.Common;

namespace MinhasFinancasAPI.Repository.Implementation
{
    public class BaseRepository : IBaseRepository
    {
        public DbConnection Connection { get; private set; }

        public BaseRepository(IConfiguration configuration) { this.Connection = configuration.FactoryConnection(EConnection.MySql); }
    }
}