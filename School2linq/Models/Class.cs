using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2linq.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Class Id")]
        public int ClassId { get; set; } = 0;

        [DisplayName("Class")]
        public string ClassName { get; set; } = default!;

        public virtual ICollection<SchoolConnection>? SchoolConnections { get; set; }

    }
}
