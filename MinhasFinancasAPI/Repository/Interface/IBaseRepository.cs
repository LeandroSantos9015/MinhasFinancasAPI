using System.Data.Common;

namespace MinhasFinancasAPI.Repository.Interface
{
    public interface IBaseRepository
    {
        public DbConnection Connection { get; }
    }
}
