using MinhasFinancasAPI.Util.Enumerator;

namespace MinhasFinancasAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public string? Token { get; set; }
        public EUserStatus Ativo { get; set; }
   
    }
}
