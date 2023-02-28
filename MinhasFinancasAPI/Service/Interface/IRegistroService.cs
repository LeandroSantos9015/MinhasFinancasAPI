using MinhasFinancasAPI.Entities;
using System.Collections.Generic;

namespace MinhasFinancasAPI.Service.Interface
{
    public interface IRegistroService
    {
        IList<Balance> Balanco(Int64 idUsuario);
        void SaveRegister(Registro registro);

        void Deletar(string id);

        IList<Registro> RetornaDiario(string date, int id);
    }
}
