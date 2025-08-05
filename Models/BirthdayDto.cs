using System.ComponentModel.DataAnnotations;

namespace Birthday_tracker.Models
{
    public class BirthdayDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length is too long")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Birthday date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthdayDate { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
