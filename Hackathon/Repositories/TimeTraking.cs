using Dapper;

namespace Hackathon.Repositories
{
    public class TimeTraking
    {
        /// <summary>
        /// Salva os dados de controle de ponto
        /// </summary>
        /// <param name="authenticatedUserId">Id do usuário autenticado</param>
        public static async Task<bool> Save(int authenticatedUserId)
        {
            string query = @"insert into time_tracking (user_id,created_at) values (@user_id, now() AT TIME ZONE 'America/Sao_Paulo')";

            var result = PostgreDB.CService.Connection().ExecuteAsync(query, new
            {
                user_id = authenticatedUserId,
            });

            return result.Result > 0;
        }


        /// <summary>
        /// Retorna um relatório dos ultimos 30 dias do cliente
        /// </summary>
        /// <param name="authenticatedUserId">Id do usuário autenticado</param>
        public static  List<Models.TimeTraking.Report30DaysResponse> Last30DaysReport(int authenticatedUserId)
        {
            string query = @"WITH ordered_points AS (
              SELECT
                user_id,
                created_at,
                ROW_NUMBER() OVER(PARTITION BY user_id, DATE(created_at) ORDER BY created_at) AS row_num
              FROM
                time_tracking
              WHERE
                created_at >= NOW() - INTERVAL '30 days' and user_id = @user_id
            ),
            pairs AS (
              SELECT
                a.user_id,
                DATE(a.created_at) AS date,
                LEAD(a.created_at) OVER(PARTITION BY a.user_id, DATE(a.created_at) ORDER BY a.created_at) - a.created_at AS time_diff,
                a.created_at AS check_in,
                LEAD(a.created_at) OVER(PARTITION BY a.user_id, DATE(a.created_at) ORDER BY a.created_at) AS check_out
              FROM
                ordered_points a
              WHERE
                a.row_num % 2 = 1
            )
            SELECT
              p.user_id,
              p.date,
              COUNT(*) AS total_checks,
              TO_CHAR(SUM(p.time_diff) :: INTERVAL, 'HH24:MI:SS') AS total_hours,
              TO_CHAR(MIN(p.check_in), 'HH24:MI:SS') AS first_check_in,
              TO_CHAR(MAX(p.check_out), 'HH24:MI:SS') AS last_check_out
            FROM
              pairs p
            GROUP BY
              p.user_id,
              p.date
            ORDER BY
              p.user_id,
              p.date;";


            return PostgreDB.CService.Connection().Query<Models.TimeTraking.Report30DaysResponse>(query, new { user_id = authenticatedUserId }).ToList();
        }
    }
}
