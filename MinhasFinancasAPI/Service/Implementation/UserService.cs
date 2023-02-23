using Microsoft.IdentityModel.Tokens;
using MinhasFinancasAPI.Entities;
using MinhasFinancasAPI.Repository.Interface;
using MinhasFinancasAPI.Service.Interface;
using MinhasFinancasAPI.Util.Enumerator;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinhasFinancasAPI.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private const string securityKey = "AchaveDeSegurancaTemQueSerMaiorQue128Bits";


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void SaveUser(User user)
        {
            _userRepository.Save(user);
        }

        public User RetornaUsuarioLogado(User user)
        {
            try
            {
                User userRetorno = VerificaLogin(user);

                if (userRetorno.Ativo.Equals(EUserStatus.Ativo))
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, userRetorno.Nome),
                        new Claim(ClaimTypes.Email, userRetorno.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "leandro",
                        audience: "leandro",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    userRetorno.Senha = null;
                    userRetorno.Token = new JwtSecurityTokenHandler().WriteToken(token);

                    return userRetorno;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }

        }

        public string VerificaToken(string token)
        {

            string semBearer = token.Replace("Bearer ", "");

            var tokeValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(semBearer, tokeValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");


            User usuario = new User
            {
                Ativo = EUserStatus.Ativo,
                Nome = principal.Identities?.FirstOrDefault()?.Name,
                Email = principal.Identities?.FirstOrDefault()?.Claims?.ToList()[1]?.Value,

            };

            return JsonConvert.SerializeObject(usuario);// principal.Identities.ToString(); 
        }

        private User VerificaLogin(User user)
        {
            return _userRepository.VerificaUsuario(user);

        }

        IEnumerable<User> IUserService.GetUsers()
        {
            return _userRepository.GetAll();
        }


    }
}