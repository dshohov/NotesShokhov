using Microsoft.EntityFrameworkCore;
using NotesShokhov.Models;

namespace NotesShokhov.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Note> Notes { get; set; }
    }
}
