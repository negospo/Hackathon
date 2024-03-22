using Dapper;
using Hackathon.ExtensionMethods;

namespace Hackathon.Repositories
{
    public static class User
    {
        /// <summary>
        /// Retorna o usuário que coincide o username e senha
        /// </summary>
        /// <param name="login">Dados para logon</param>
        public static async Task<Models.Authenticate.User> Logon(Models.Authenticate.Request request)
        {
            string query = @"select id,name,email 
            from public.user 
            where 
            email = @email and password = @password";

            var user = await PostgreDB.DB.Connection().QueryFirstOrDefaultAsync<Models.Authenticate.User>(query, new
            {
                email = request.Email,
                password = request.Password.Encrypt()
            });

            return user;
        }
    }
}
