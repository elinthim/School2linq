using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace School2linq.Models
{
    public class SearchSchoolViewModel
    {
        [DisplayName("Teacher Id")]
        public int? TeacherId { get; set; }
        [DisplayName("Teacher")]
        public string? TeacherFirstName { get; set; }
        public string? TeacherLastName { get; set; }


        [DisplayName("Student Id")]
        public int? StudentId { get; set; }
        [DisplayName("Student")]
        public string? StudentFirstName { get; set; }
        public string? StudentLastName { get; set; }


        [DisplayName("Course Id")]
        public int CourseId { get; set; }

        [DisplayName("Course Name")]
        public string? CourseName { get; set; }


        [DisplayName("Class Id")]
        public int ClassID { get; set; }

        [DisplayName("Class Name")]
        public string? ClassName { get; set; }
    }
}
