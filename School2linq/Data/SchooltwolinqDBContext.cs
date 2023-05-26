using Microsoft.EntityFrameworkCore;
using School2linq.Models;

namespace School2linq.Data
{

    public class SchooltwolinqDBContext : DbContext
    {
        public SchooltwolinqDBContext(DbContextOptions<SchooltwolinqDBContext> options)
        : base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolConnection> SchoolConnections { get; set; }
    }
}

