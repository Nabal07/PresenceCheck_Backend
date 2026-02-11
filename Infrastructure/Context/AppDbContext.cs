using Microsoft.EntityFrameworkCore;
using PresenceCheck.Api.Domain;

namespace PresenceCheck.Api.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Convidado> Convidados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Convidado>()
            .ToTable("convidados");

        modelBuilder.Entity<Convidado>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Convidado>()
            .Property(c => c.Id).HasColumnName("id");

        modelBuilder.Entity<Convidado>()
            .Property(c => c.Nome).HasColumnName("nome");

        modelBuilder.Entity<Convidado>()
            .Property(c => c.Confirmado).HasColumnName("confirmado");

        modelBuilder.Entity<Convidado>()
            .Property(c => c.DataConfirmacao).HasColumnName("dataconfirmacao");
    }
}