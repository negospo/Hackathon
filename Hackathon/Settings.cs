namespace Hackathon
{
    public static class Settings
    {
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Ambiente
        /// </summary>
        public static string AspnetcoreEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


        /// <summary>
        /// String de conexão com o Postgre
        /// </summary>
        public static string PostgreConnStringCService => Environment.GetEnvironmentVariable("POSTGRE_CONNECTION_STRING");

        /// <summary>
        /// String de conexão com o Redis
        /// </summary>
        public static string GCPAuthFilePath => Environment.GetEnvironmentVariable("GCP_AUTH_FILE_PATH");
    }
}
