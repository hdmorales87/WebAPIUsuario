using Microsoft.EntityFrameworkCore;
using WebAPIUsuario.Models;

namespace WebAPIUsuario.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Apellidos).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion);
        });
    }
}
