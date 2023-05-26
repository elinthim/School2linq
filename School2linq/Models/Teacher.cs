using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2linq.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; } = 0;

        [Required]
        [StringLength(30)]
        [DisplayName("First Name")]
        public string TeacherFirstName { get; set; } = default!;
        [Required]
        [StringLength(30)]
        [DisplayName("Last Name")]
        public string TeacherLastName { get; set; } = default!;

        public virtual ICollection<SchoolConnection>? SchoolConnections { get; set; }






    }
}
