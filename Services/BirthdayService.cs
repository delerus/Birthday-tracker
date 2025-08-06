using Birthday_tracker.Data;
using Birthday_tracker.Models;
using Birthday_tracker.Utils;
using Birthday_tracker.Repositories;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Birthday_tracker.Services
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IBirthdayRepository _repository;

        public BirthdayService(IBirthdayRepository repository)
        {
            _repository = repository;
        }

        public Task AddBirthdayAsync(Birthday b)
        {
            return _repository.AddAsync(b);
        }

        public async Task DeleteBirthdayAsync(int id)
        {
            Birthday birthday = await _repository.FindAsync(id);

            if (birthday != null)
            {
                await _repository.DeleteAsync(birthday);

                if (birthday.Image != "default.jpg")
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", birthday.Image);
                    File.Delete(imagePath);
                }
            }
            else throw new Exception("Data couldn't be found");

        }

        public async Task<Birthday?> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public Task UpdateBirthdayAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Birthday>> GetFilteredAsync(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
                (date1, date2) = (date2, date1);

            return _repository.GetFilteredByDateAsync(date1, date2);
        }

        public Task<List<Birthday>> GetSortedAsync(string field)
        {
            var allowedFields = new[] { "name", "id", "date" };
            if (!allowedFields.Contains(field))
                throw new ArgumentException("Invalid sorting field.");

            return _repository.GetSortedAsync(field);
        }

        public Task<List<Birthday>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<List<Birthday>> GetUpcomingBirthdaysAsync(int daysAhead = 7)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = date1.AddDays(daysAhead);
            return _repository.GetFilteredByDateAsync(date1, date2);
        }

        public Task<List<Birthday>> GetBirthdaysInMonthAsync(int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Month must be between 1 and 12.");
            DateTime date1 = new DateTime(DateTime.Now.Year, month, 1);
            DateTime date2 = new DateTime(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));

            return _repository.GetFilteredByDateAsync(date1, date2);
        }

        public async Task AddBirthdayAsync(BirthdayDto dto)
        {
            string imageName = "default.jpg";

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                if (!ImageValidator.IsImage(dto.ImageFile))
                    throw new Exception("Uploaded file is not a supported image type.");

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                while (System.IO.File.Exists(imagePath))
                {
                    uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                }

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                imageName = uniqueFileName;
            }

            var birthday = new Birthday
            {
                Name = dto.Name,
                BirthdayDate = dto.BirthdayDate,
                Image = imageName
            };

            await _repository.AddAsync(birthday);
        }

        public async Task UpdateBirthdayAsync(Birthday birthday, BirthdayDto dto)
        {
            string imageName = birthday.Image;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                if (!ImageValidator.IsImage(dto.ImageFile))
                    throw new Exception("Uploaded file is not a supported image type.");

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

                while (System.IO.File.Exists(imagePath))
                {
                    uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);
                }

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                if (birthday.Image != "default.jpg")
                {
                    var imageToDeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", birthday.Image);
                    if (File.Exists(imageToDeletePath))
                        File.Delete(imageToDeletePath);
                }

                imageName = uniqueFileName;
            }

            var updatedBirthday = new Birthday
            {
                Id = birthday.Id,
                Name = dto.Name,
                BirthdayDate = dto.BirthdayDate,
                Image = imageName
            };

            await _repository.UpdateAsync(birthday, updatedBirthday);
        }
    }
}
