using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using MinhasFinancasAPI.Entities;
using MinhasFinancasAPI.Repository.Implementation;
using MinhasFinancasAPI.Repository.Interface;
using MinhasFinancasAPI.Service.Interface;
using MinhasFinancasAPI.Util.Enumerator;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhasFinancasAPI.Service.Implementation
{
    public class RegistroService : IRegistroService
    {
        IRegistroRepository _registroRepository;

        public RegistroService(IRegistroRepository registroRepository)
        {
            _registroRepository = registroRepository;
        }

        public void SaveRegister(Registro registro)
        {
            registro.Date = registro.Date.Replace(" ", "T");

            _registroRepository.SaveRegister(registro);
        }

        public void Deletar(string id)
        {
            string idFormatada = id.Replace("?id=", "");

            _registroRepository.ApagarRegistro(Convert.ToInt32(idFormatada));   
        }

        public IList<Balance> Balanco(Int64 idUsuario)
        {
            var prepara = _registroRepository.Balanco(idUsuario);

            IList<Balance> balances = new List<Balance>();

            var receita =
                new Balance
                {
                    Saldo = prepara.Where(x => x.Tipo.Equals("receita")).Sum(y => y.Valor),
                    Tag = "receita"
                };

            var despesa =
                new Balance
                {
                    Saldo = prepara.Where(x => x.Tipo.Equals("despesa")).Sum(y => y.Valor),
                    Tag = "despesa"
                };

            var saldo =
                new Balance
                {
                    Saldo = receita.Saldo - despesa.Saldo,
                    Tag = "saldo"
                };

            balances.Add(saldo);
            balances.Add(receita);
            balances.Add(despesa);


            return balances;
        }

        public IList<Registro> RetornaDiario(string date, int id)
        {
            string dataFormatada = date.Replace("%2F", "-").Replace("?date=", "");

            return _registroRepository.RetornarDiario(Convert.ToDateTime(dataFormatada).ToString("yyyy-MM-dd"), id);
        }
    }
}