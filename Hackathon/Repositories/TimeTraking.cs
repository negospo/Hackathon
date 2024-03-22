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

            var result = PostgreDB.DB.Connection().ExecuteAsync(query, new
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
            DateTime start = DateTime.Now.Date.AddDays(-30);
            DateTime end = DateTime.Now.Date.AddDays(1);

            string query = @"WITH ordered_checks AS (
            SELECT
                user_id,
                created_at,
                LEAD(created_at) OVER(PARTITION BY user_id, DATE(created_at) ORDER BY created_at) AS next_created_at,
                ROW_NUMBER() OVER(PARTITION BY user_id, DATE(created_at) ORDER BY created_at) AS rn,
                DATE(created_at) AS work_date
            FROM
                time_tracking
            WHERE
                created_at BETWEEN @start and @end
                AND EXTRACT(ISODOW FROM created_at) BETWEEN 1 AND 5 -- Somente dias úteis (Segunda a Sexta)
                AND user_id = @user_id
        ), work_sessions AS (
            SELECT
                user_id,
                work_date,
                MIN(created_at) AS first_check_in,
                MAX(next_created_at) AS last_check_out,
                SUM(next_created_at - created_at) FILTER (WHERE rn % 2 = 1) AS work_duration, -- Soma as durações de trabalho
                COUNT(*) / 2 AS total_intervals -- Total de intervalos
            FROM
                ordered_checks
            GROUP BY
                user_id, work_date
        )
        SELECT
            user_id,
            work_date,
            TO_CHAR(first_check_in, 'HH24:MI:SS') AS first_check_in_time,
            TO_CHAR(last_check_out, 'HH24:MI:SS') AS last_check_out_time,
            TO_CHAR(work_duration, 'HH24:MI:SS') AS total_hours_worked,
            EXTRACT(EPOCH FROM work_duration) AS total_seconds_worked,
            TO_CHAR((last_check_out - first_check_in) - work_duration, 'HH24:MI:SS') AS total_break_duration,
            EXTRACT(EPOCH FROM (last_check_out - first_check_in) - work_duration) AS total_seconds_break,
            total_intervals / 2 as total_intervals
        FROM
            work_sessions
        ORDER BY
            work_date asc;";


            return PostgreDB.DB.Connection().Query<Models.TimeTraking.Report30DaysResponse>(query, new { user_id = authenticatedUserId, start = start, end = end }).ToList();
        }

        /// <summary>
        /// Retorna um relatorio fechado do mes anterior
        /// </summary>
        /// <param name="authenticatedUserId">Id do usuário autenticado</param>
        public static List<Models.TimeTraking.ReportSend> ReportSend(int authenticatedUserId)
        {

            var firtDayActualMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);


            DateTime start = firtDayActualMonth.AddMonths(-1);
            DateTime end = firtDayActualMonth.AddDays(-1);


            string query = @"WITH ordered_checks AS (
            SELECT
                user_id,
                created_at,
                LEAD(created_at) OVER(PARTITION BY user_id, DATE(created_at) ORDER BY created_at) AS next_created_at,
                ROW_NUMBER() OVER(PARTITION BY user_id, DATE(created_at) ORDER BY created_at) AS rn,
                DATE(created_at) AS work_date
            FROM
                time_tracking
            WHERE
                created_at BETWEEN @start and @end
                AND EXTRACT(ISODOW FROM created_at) BETWEEN 1 AND 5 -- Somente dias úteis (Segunda a Sexta)
                AND user_id = @user_id
        ), work_sessions AS (
            SELECT
                user_id,
                work_date,
                MIN(created_at) AS first_check_in,
                MAX(next_created_at) AS last_check_out,
                SUM(next_created_at - created_at) FILTER (WHERE rn % 2 = 1) AS work_duration, -- Soma as durações de trabalho
                COUNT(*) / 2 AS total_intervals -- Total de intervalos
            FROM
                ordered_checks
            GROUP BY
                user_id, work_date
        )
        SELECT
            user_id,
            work_date,
            TO_CHAR(first_check_in, 'HH24:MI:SS') AS first_check_in_time,
            TO_CHAR(last_check_out, 'HH24:MI:SS') AS last_check_out_time,
            TO_CHAR(work_duration, 'HH24:MI:SS') AS total_hours_worked,
            EXTRACT(EPOCH FROM work_duration) AS total_seconds_worked,
            TO_CHAR((last_check_out - first_check_in) - work_duration, 'HH24:MI:SS') AS total_break_duration,
            EXTRACT(EPOCH FROM (last_check_out - first_check_in) - work_duration) AS total_seconds_break,
            total_intervals / 2 as total_intervals
        FROM
            work_sessions
        ORDER BY
            work_date asc;";

            return PostgreDB.DB.Connection().Query<Models.TimeTraking.ReportSend>(query, new { user_id = authenticatedUserId, start = start, end = end }).ToList();
        }
    }
}
