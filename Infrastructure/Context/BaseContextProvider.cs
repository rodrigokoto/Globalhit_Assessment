using Core.DomainModels;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context
{


    public class BaseContextGlobalhit : DbContext
    {

        public BaseContextGlobalhit(DbContextOptions<BaseContextGlobalhit> options) : base(options)
        {

        }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

        public DbSet<PaymentFlowSummary> paymentFlowSummaries { get; set; }
        public DbSet<PaymentSchedule> paymentSchedules { get; set; }

        public DbSet<ProposalPaymentFlowSummary> proposalPaymentFlowSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelBuilder

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nome_Complemento)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rg)
                    .HasMaxLength(20);

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11);

                entity.Property(e => e.Email)
                    .HasMaxLength(255);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(20);

                entity.Property(e => e.Data_Alteracao)
                    .HasColumnType("datetime");

                entity.Property(e => e.Data_Cadastro)
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.ToTable("Proposal");

               
                entity.HasKey(e => e.Id);

               
                entity.Property(e => e.LoanAmount)
                    .IsRequired(); 

                entity.Property(e => e.AnnualInterestRate)
                    .IsRequired(); 

                entity.Property(e => e.NumberOfMonths)
                    .IsRequired(); 

                entity.HasMany(e => e.ProposalPaymentFlowSummaries)
                    .WithOne(link => link.Proposal) 
                    .HasForeignKey(link => link.ProposalId) 
                    .OnDelete(DeleteBehavior.Cascade); 

            });

            modelBuilder.Entity<PaymentFlowSummary>(entity =>
            {
                entity.ToTable("PaymentFlowSummary");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.MonthlyPayment)
                    .HasColumnType("float")
                    .IsRequired();

                entity.Property(e => e.TotalInterest)
                    .HasColumnType("float")
                    .IsRequired();

                entity.Property(e => e.TotalPayment)
                    .HasColumnType("float")
                    .IsRequired();


                entity.HasMany(e => e.PaymentSchedules)
                    .WithOne(schedule => schedule.PaymentFlowSummary)
                    .HasForeignKey(schedule => schedule.PaymentSummaryId)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(e => e.ProposalPaymentFlowSummaries)
                    .WithOne(link => link.PaymentFlowSummary)
                    .HasForeignKey(link => link.PaymentFlowSummaryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PaymentSchedule>(entity =>
            {
                entity.ToTable("PaymentSchedule");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Principal)
                    .HasColumnType("float")
                    .IsRequired();

                entity.Property(e => e.Balance)
                    .HasColumnType("float")
                    .IsRequired();

                entity.Property(e => e.Interest)
                    .HasColumnType("float2")
                    .IsRequired();

                // Ajuste no relacionamento, incluindo chave estrangeira
                entity.HasOne(e => e.PaymentFlowSummary)
                    .WithMany(summary => summary.PaymentSchedules)
                    .HasForeignKey(e => e.PaymentSummaryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProposalPaymentFlowSummary>(entity =>
            {
               
                entity.ToTable("ProposalPaymentFlowSummary");

               
                entity.HasKey(e => new { e.ProposalId, e.PaymentFlowSummaryId });

               
                entity.HasOne(e => e.Proposal)
                    .WithMany(p => p.ProposalPaymentFlowSummaries) 
                    .HasForeignKey(e => e.ProposalId) 
                    .IsRequired() 
                    .OnDelete(DeleteBehavior.Cascade); 

                
                entity.HasOne(e => e.PaymentFlowSummary)
                    .WithMany(pfs => pfs.ProposalPaymentFlowSummaries) 
                    .HasForeignKey(e => e.PaymentFlowSummaryId) 
                    .IsRequired() 
                    .OnDelete(DeleteBehavior.Cascade); 
            });


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=GLobalhit; Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True;TrustServerCertificate=True;Trusted_Connection=True");
            }
        }

    }
}

