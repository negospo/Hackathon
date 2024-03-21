using Dapper;
using System.Data;

namespace Hackathon.Converters
{
    public class DapperHandler
    {
        public DapperHandler()
        {
            SqlMapper.AddTypeHandler(new ListStringTypeHandler());
        }

        private class ListStringTypeHandler : SqlMapper.TypeHandler<List<string>>
        {
            public override List<string> Parse(object value)
            {
                string json = value.ToString();
                if (json == string.Empty)
                    return null;

                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json);
            }

            public override void SetValue(IDbDataParameter parameter, List<string> value)
            {
                parameter.Value = value;
            }
        }
    }
}
