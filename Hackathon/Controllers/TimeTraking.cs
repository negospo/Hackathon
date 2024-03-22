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
        /// Salva um controle de ponto do usuário
        /// </summary>
        [HttpPost]
        [Route("check")]
        [Authorize]
        public async Task<ActionResult<bool>> Save()
        {
            int authenticatedUserId = Convert.ToInt32(this.User.FindFirstValue("Id"));
            var sucess = await Repositories.TimeTraking.Save(authenticatedUserId);
            return Ok(sucess);
        }

        /// <summary>
        /// Retorna um relatório dos ultimos 30 dias do usuário
        /// </summary>
        [HttpGet]
        [Route("Last30DaysReport")]
        [Authorize]
        public async Task<ActionResult<List<Models.TimeTraking.Report30DaysResponse>>> Last30DaysReport()
        {
            int authenticatedUserId = Convert.ToInt32(this.User.FindFirstValue("Id"));

            var result = Repositories.TimeTraking.Last30DaysReport(authenticatedUserId);
            return Ok(result);
        }


        /// <summary>
        /// Solicita por email o relatório de ponto do usuário do ultimo mês
        /// </summary>
        [HttpGet]
        [Route("SendReport")]
        [Authorize]
        public async Task<ActionResult<bool>> ReportSend()
        {
            int authenticatedUserId = Convert.ToInt32(this.User.FindFirstValue("Id"));
            string name = this.User.FindFirstValue("Name");
            string email = this.User.FindFirstValue("Email");

            var result = Repositories.TimeTraking.ReportSend(authenticatedUserId);

            var templateEmail = Hackathon.Resource.TemplateEmail;
            var templateData = Hackathon.Resource.ItemData;
            string dataItems = "";

            result.ForEach(item =>
            {
                string itemData = templateData;
                itemData = itemData.Replace("{date}", item.WorkDate.ToString("dd/MM/yyyy"));
                itemData = itemData.Replace("{hours}", item.TotalHoursWorked);
                itemData = itemData.Replace("{intervals}", item.TotalIntervals.ToString());
                itemData = itemData.Replace("{intervalsDuration}", item.TotalBreakDuration);
                itemData = itemData.Replace("{firstCheck}", item.FirstCheckInTime);
                itemData = itemData.Replace("{lasctCheck}", item.LastCheckOutTime);
                dataItems += itemData;
            });

            templateEmail = templateEmail.Replace("{dataItems}", dataItems);
            templateEmail = templateEmail.Replace("{hotalHours}", SecondsToFormattedTime(result.Sum(s => s.TotalSecondsWorked)));
            templateEmail = templateEmail.Replace("{month}", MonthName());

            SendGrid.Wrapper.SendEmail(templateEmail, email, name);


            return Ok(true);



            string SecondsToFormattedTime(int totalSeconds)
            {
                int hours = totalSeconds / 3600;
                int minutes = (totalSeconds % 3600) / 60;
                int seconds = totalSeconds % 60;

                // Retorna a string formatada
                return $"{hours}:{minutes:D2}:{seconds:D2}";
            }

            string MonthName()
            {
                int lastMonth = DateTime.Now.Month - 1;
                if (lastMonth < 1)
                    lastMonth = 12;


                switch (lastMonth)
                {
                    case 1: return "Janeiro";
                    case 2: return "Fevereiro";
                    case 3: return "Março";
                    case 4: return "Abril";
                    case 5: return "Maio";
                    case 6: return "Junho";
                    case 7: return "Julho";
                    case 8: return "Agosto";
                    case 9: return "Setembro";
                    case 10: return "Outubro";
                    case 11: return "Novembro";
                    case 12: return "Dezembro";
                    default: return "Mês inválido";
                }
            }
        }

    }
}
