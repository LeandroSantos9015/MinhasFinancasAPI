using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhasFinancasAPI.Entities;
using MinhasFinancasAPI.Service.Implementation;
using MinhasFinancasAPI.Service.Interface;

namespace MinhasFinancasAPI.Controllers
{
    [Route("api/[controller]")]
    //  [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                var retorno = _userService.GetUsers();

                if (retorno == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("me")]
        public IActionResult GetDataByToken()
        {
            try
            {
                //var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

                Request.Headers.ToString();

                var token = Request.Headers["Authorization"];

                var retorno = _userService.VerificaToken(token);

                if (retorno == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }


        [HttpPost]
        [Route("save")]
        public IActionResult Save(User user)
        {
            try
            {
                _userService.SaveUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public IActionResult Auth(User user)
        {
            try
            {
                var retorno = _userService.RetornaUsuarioLogado(user);

                //if(retorno == null)
                //    return new StatusCodeResult(400);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("testar")]
        public IActionResult Testador()
        {
            try
            {
                return Ok("VALIDADO");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro XXXXX");
                return new StatusCodeResult(500);
            }

        }

    }
}
