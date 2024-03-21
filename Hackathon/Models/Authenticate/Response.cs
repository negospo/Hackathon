namespace Hackathon.Models.Authenticate
{
    public class Response
    {
        /// <summary>
        /// Login do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Json Web Token
        /// </summary>
        public JWT JWT { get; set; }
    }
}
