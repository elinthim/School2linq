using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace School2linq.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Course Id")]
        public int CourseId { get; set; } = 0;

        [Required]
        [StringLength(30)]
        [DisplayName("Course name")]
        public string CourseName { get; set; } = default!;

        public virtual ICollection<SchoolConnection>? SchoolConnections { get; set; }
    }
}
