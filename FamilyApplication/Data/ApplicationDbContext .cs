using Microsoft.EntityFrameworkCore;
using FamilyApplication.Models;
using FamilyApplication.Enums;

namespace FamilyApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Family> Family { get; set; }
        public DbSet<FamilyGroup> FamilyGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Member
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Members");
                entity.HasKey(e => e.MemberId);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.familyCategory)
                    .HasConversion<string>();

                entity.Property(e => e.BirthDate)
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp without time zone");
                // REMOVER HasDefaultValueSql - vamos setar no código

                // 👇 CORREÇÃO: Não usar IsRequired(false) pois a propriedade já é nullable
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("timestamp without timeout zone");
                // Não precisa de IsRequired, pois DateTime? já indica que é nullable
            });

            // Configurar FamilyGroup (se tiver DeletionDate como nullable)
            modelBuilder.Entity<FamilyGroup>(entity =>
            {
                entity.ToTable("FamilyGroup");
                entity.HasKey(e => e.FamilyGroupId);

                entity.Property(e => e.FamilyGroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp without time zone");

                // Se FamilyGroup.DeletionDate for DateTime? (nullable)
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("timestamp without time zone");
            });

            // Configurar Family (se tiver DeletionDate como nullable)
            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("Family");
                entity.HasKey(e => e.FamilyId);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp with time zone");

                // Se Family.DeletionDate for DateTime? (nullable)
                entity.Property(e => e.DeletionDate)
                    .HasColumnType("timestamp with time zone");
            });
        }
    }
}