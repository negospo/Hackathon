using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hackathon.Controllers
{
    [ApiController]
    [Route("TimeTraking")]
    public class TimeTraking : ControllerBase
    {
        /// <summary>
        /// Recebe as credenciais e faz a autenticação na aplicação. Em caso de sucesso retorna um objeto com o json web token
        /// </summary>
        /// <param name="login">Credenciais de acesso</param>
        [HttpPost]
        [Route("logon")]
        [Middlewares.CustonValidateModel]
        [Authorize]
        public async Task<ActionResult<bool>> Save(Models.Authenticate.Request request)
        {
            int authenticatedUserId = Convert.ToInt32(this.User.FindFirstValue("Id"));
            var sucess = await Repositories.TimeTraking.Save(authenticatedUserId);
            return Ok(sucess);
        }


        [HttpGet]
        [Route("Last30DaysReport")]
        [Middlewares.CustonValidateModel]
       // [Authorize]
        public async Task<ActionResult<List<Models.TimeTraking.Report30DaysResponse>>> Last30DaysReport()
        {
            // int authenticatedUserId = Convert.ToInt32(this.User.FindFirstValue("Id"));
            int authenticatedUserId = 1;

            var result = Repositories.TimeTraking.Last30DaysReport(authenticatedUserId);
            return Ok(result);
        }
    }
}
