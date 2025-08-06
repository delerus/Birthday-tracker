using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Birthday_tracker.Repositories
{
    public class BirthdayRepository : IBirthdayRepository
    {
        private readonly ApplicationDbContext _context;

        public BirthdayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Birthday birthday)
        {
            _context.Birthdays.Add(birthday);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Birthday birthday)
        {
            _context.Birthdays.Remove(birthday);
            await _context.SaveChangesAsync();
        }

        public Task<List<Birthday>> GetAllAsync()
        {
            return _context.Birthdays.ToListAsync();
        }

        public async Task<List<Birthday>> GetFilteredByDateAsync(DateTime date1, DateTime date2)
        {
            date1 = date1.Date;
            date2 = date2.Date;

            var result = new List<Birthday>();
            var birthdays = await _context.Birthdays.ToListAsync();

            foreach (var birthday in birthdays)
            {
                var birthDay = birthday.BirthdayDate.Day;
                var birthMonth = birthday.BirthdayDate.Month;

                var thisYearBirthday = new DateTime(date1.Year, birthMonth, birthDay);
                var nextYearBirthday = new DateTime(date1.Year + 1, birthMonth, birthDay);

                if ((thisYearBirthday >= date1 && thisYearBirthday <= date2) ||
                    (nextYearBirthday >= date1 && nextYearBirthday <= date2))
                {
                    result.Add(birthday);
                }
            }
            return result.OrderBy(b => (b.BirthdayDate.Month, b.BirthdayDate.Day)).ToList();
        }

        public async Task<List<Birthday>> GetSortedAsync(string field)
        {
            return field switch
            {
                "name" => await _context.Birthdays.OrderBy(b => b.Name).ToListAsync(),
                "id" => await _context.Birthdays.OrderBy(b => b.Id).ToListAsync(),
                _ => await _context.Birthdays.OrderBy(b => b.BirthdayDate).ToListAsync()
            };
        }

        public Task<Birthday?> FindAsync(string name, DateTime date)
        {
            return _context.Birthdays.FirstOrDefaultAsync(b => b.Name == name && b.BirthdayDate == date);
        }

        public Task<Birthday?> FindAsync(int id)
        {
            return _context.Birthdays.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Birthday birthday, Birthday updatedBirthday)
        {
            birthday.Name = updatedBirthday.Name;
            birthday.BirthdayDate = updatedBirthday.BirthdayDate;
            birthday.Image = updatedBirthday.Image;

            await _context.SaveChangesAsync();
        }
    }
}
