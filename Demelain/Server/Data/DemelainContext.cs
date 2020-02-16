using Demelain.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demelain.Server.Data
{
    public partial class NexusContext : DbContext
    {
        public NexusContext()
        {
        }

        public NexusContext(DbContextOptions<NexusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PersonalDetails> PersonalDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http: //go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(
                    "Data Source=Lenovo-310\\SQLEXPRESS;Initial Catalog=Nexus;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalDetails>(entity =>
            {
                entity.Property(e => e.Biography)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MiddleName).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}