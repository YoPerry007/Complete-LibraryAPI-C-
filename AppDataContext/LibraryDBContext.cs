using libraryAPI.Models;
using LibraryAPI.Models;
using LibraryAPI.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibraryAPI.AppDataContext;

public class LibraryDBContext : DbContext
{
    private readonly DBSettings? _dbSettings;

    public LibraryDBContext(IOptions<DBSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
    : base(options)
    {
        // _dbSettings = dbSettings.Value;
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_dbSettings != null)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        base.OnModelCreating(modelBuilder);
    }
}
