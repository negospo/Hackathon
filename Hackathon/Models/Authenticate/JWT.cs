namespace Hackathon.Models.Authenticate
{
    public class JWT
    {
        /// <summary>
        /// Token de acesso
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Token para refresh
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Tipo do Token
        /// </summary>
        public string TokenType { get => "bearer"; }
        /// <summary>
        /// Expiração do token (UTC)
        /// </summary>
        public DateTime? Expires { get; set; }
    }
}
