using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MinhasFinancasAPI.Entities;
using MinhasFinancasAPI.Service.Implementation;
using MinhasFinancasAPI.Service.Interface;
using Newtonsoft.Json;

namespace MinhasFinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RegistroController : ControllerBase
    {

        private readonly IRegistroService _registroService;
        private readonly ILogger<RegistroController> _logger;

        private readonly IUserService _userService;


        public RegistroController(IRegistroService regService, ILogger<RegistroController> logger, IUserService userService)
        {

            _registroService = regService;
            _logger = logger;
            _userService = userService;

        }

        [HttpGet]
        [Route("balance")]
        public IActionResult Balance()
        {
            try
            {
                var balances = _registroService.Balanco(IdUsuario());

                return Ok(balances);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("receives")]
        public IActionResult RecebidosDoDia()
        {
            try
            {
                var get = Request.QueryString.Value;

                var balances = _registroService.RetornaDiario(get, IdUsuario());

                return Ok(balances);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete()
        {

            var get = Request.QueryString.Value;

            _registroService.Deletar(get);

            return Ok();

        }


        [HttpPost]
        [Route("receive")]
        public IActionResult Registrar(Registro registro)
        {
            try
            {
                registro.IdUsuario = IdUsuario();

                _registroService.SaveRegister(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }


        private int IdUsuario()
        {
            var token = Request.Headers["Authorization"];

            // pra fazer a consulta no banco
            var retorno = _userService.VerificaToken(token);

            User usuario = JsonConvert.DeserializeObject<User>(retorno);

            return usuario.Id;

        }

    }
}
