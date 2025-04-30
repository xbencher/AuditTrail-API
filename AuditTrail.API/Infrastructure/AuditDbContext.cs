using AuditTrail.Core.Enums;
using AuditTrail.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace AuditTrail.API.Infrastructure
{
    public class AuditDbContext : DbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AuditDbContext(DbContextOptions<AuditDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new EnumToStringConverter<AuditAction>();
            modelBuilder.Entity<AuditLog>().Property(a => a.Action).HasConversion(converter);
        }
    }
}
