using Microsoft.EntityFrameworkCore;
using MVC_CRUD_Operation_Practice.Models;
namespace MVC_CRUD_Operation_Practice.Entities

{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }
    }
}
