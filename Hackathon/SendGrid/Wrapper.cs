using SendGrid;
using SendGrid.Helpers.Mail;

namespace Hackathon.SendGrid
{
    public abstract class Wrapper
    {
        public static async Task<bool> SendEmail(string htmlReport, string email, string name)
        {

            string tk = System.Text.Encoding.UTF8.GetString((Convert.FromBase64String("U0cuU3ZIV3BxSW1SS0NxUklJUTduWDNPZy5zVkpuTlA5RFZwMFNPRTBhMktlRTdRMFlWaDN3eG4yYWtLNHR4cHpXT2NJ")));

            try
            {
                var client = new SendGridClient(tk);
                var from = new EmailAddress("carlos@skysoftware.com.br", "Hackathon FIAP");
                var to = new EmailAddress(email, name);
                var subject = $"Relatório de Horas";
                subject = subject.Normalize(System.Text.NormalizationForm.FormKD);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlReport);
                var response = client.SendEmailAsync(msg).Result;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return false;
            }
        }
    }
}
