using MinhasFinancasAPI.Entities;

namespace MinhasFinancasAPI.Repository.Interface
{
    public interface IRegistroRepository
    {
        IList<Registro> Balanco(long idUsuario);

        bool SaveRegister(Registro registro);

        IList<Registro> RetornarDiario(string data, int idUsuario);

        bool ApagarRegistro(int id);

    }
}



