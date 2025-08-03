namespace Birthday_tracker.Models
{
    public class NearBirthdaysViewModel
    {
        public IEnumerable<Birthday> UpcomingBirthdays { get; set; }
        public IEnumerable<Birthday> MonthlyBirthdays { get; set; }
    }
}
