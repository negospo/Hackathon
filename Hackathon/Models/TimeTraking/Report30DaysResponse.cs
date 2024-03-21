namespace Hackathon.Models.TimeTraking
{
    public class Report30DaysResponse
    {
        public DateTime Date { get; set; }
        public int TotalChecks { get; set; }
        public string TotalHours { get; set; }
        public string FirstCheckIn { get; set; }
        public string LastCheckOut { get; set; }
    }
}
