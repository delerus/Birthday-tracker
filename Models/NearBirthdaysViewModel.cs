namespace Birthday_tracker.Models
{
    public class NearBirthdaysViewModel
    {
        public IEnumerable<Birthday> TodayBirthdays { get; set; }
        public IEnumerable<Birthday> UpcomingBirthdays { get; set; }
    }
}
