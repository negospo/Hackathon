using Npgsql;

namespace Hackathon.Repositories.PostgreDB
{
    public static class DB
    {
        public static NpgsqlConnection Connection()
        {
            return new NpgsqlConnection(Settings.PostgreConnStringCService);
        }

        public static NpgsqlCommand Command(string script, NpgsqlConnection connection)
        {
            return new NpgsqlCommand(script, connection);
        }
    }
}
