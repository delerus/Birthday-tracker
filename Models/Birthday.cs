using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Birthday_tracker.Data;

namespace Birthday_tracker.Models
{
    public class Birthday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthdayDate { get; set; }

        [StringLength(255)]
        public string? Image { get; set; }
    }
}
