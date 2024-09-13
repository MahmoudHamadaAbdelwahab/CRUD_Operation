using Microsoft.EntityFrameworkCore;
using Student_Crud.Models.Entities;

namespace Student_Crud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
            
        }

        public DbSet<Student> TbStudent {  get; set; }
    }
}
