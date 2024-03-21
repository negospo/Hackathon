using Npgsql;

namespace Hackathon.Repositories.PostgreDB
{
    public static class CService
    {
        public static NpgsqlConnection Connection()
        {
            return new NpgsqlConnection(Settings.PostgreConnStringCService);
        }
    }
}
