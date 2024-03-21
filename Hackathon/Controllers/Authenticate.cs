using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers
{
    [ApiController]
    [Route("Authenticate")]
    public class Authenticate : ControllerBase
    {
        /// <summary>
        /// Recebe as credenciais e faz a autenticação na aplicação. Em caso de sucesso retorna um objeto com o json web token
        /// </summary>
        /// <param name="login">Credenciais de acesso</param>
        [HttpPost]
        [Route("logon")]
        [Middlewares.CustonValidateModel]
        public async Task<ActionResult<Models.Authenticate.Response>> Logon(Models.Authenticate.Request request)
        {
            var user = await Repositories.User.Logon(request);
            if (user == null)
                return Unauthorized(new { message = "Username or Password is invalid" });

            var token = TokenService.GenerateToken(user);

            return new Models.Authenticate.Response
            {
                Email = user.Email,
                Name = user.Name,
                JWT = token
            };
        }


        /// <summary>
        /// Retorna status 200 se o usuário estiver autenticado
        /// </summary>
        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado como: {this.User.Identity.Name}";
    }
}
