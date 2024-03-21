namespace Hackathon.Middlewares
{
    public class CustonValidationError
    {
        public string Field { get; set; }

        public string Message { get; set; }

        public CustonValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}