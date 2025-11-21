using Microsoft.EntityFrameworkCore;
using APImovil3.Models;

namespace APImovil3.Data;

/// <summary>
/// Contexto de base de datos para Entity Framework Core
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Constructor que recibe las opciones de configuración
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// DbSet para la entidad Role
    /// </summary>
    public DbSet<Role> Roles { get; set; }

    /// <summary>
    /// DbSet para la entidad User
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Configuración del modelo de datos
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la entidad Role
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Description)
                .HasMaxLength(200);
        });

        // Configuración de la entidad User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            entity.HasIndex(e => e.Email)
                .IsUnique();
            entity.Property(e => e.PasswordHash)
                .IsRequired();

            // Relación con Role
            entity.HasOne(e => e.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

