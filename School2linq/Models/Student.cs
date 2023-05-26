using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace School2linq.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; } = 0;

        [Required]
        [StringLength(30)]
        [DisplayName("First Name")]
        public string StudentFirstName { get; set; } = default!;

        [Required]
        [StringLength(30)]
        [DisplayName("Last Name")]
        public string StudentLastName { get; set; } = default!;

        public virtual ICollection<SchoolConnection>? SchoolConnections { get; set; }
    }
}
