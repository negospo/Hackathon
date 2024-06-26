﻿namespace Hackathon.Models.TimeTraking
{
    public class Report30DaysResponse
    {
        public DateTime WorkDate { get; set; }
        public int TotalIntervals { get; set; }
        public string TotalHoursWorked { get; set; }
        public string FirstCheckInTime { get; set; }
        public string LastCheckOutTime { get; set; }
        public string TotalBreakDuration { get; set; }
    }
}
