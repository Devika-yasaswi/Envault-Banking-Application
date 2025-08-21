using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoreModels.Entities
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(){}
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public virtual DbSet<BasicDetailsEntity> BasicDetails { get; set; }
        public virtual DbSet<CustomerAddressEntity> CustomerAddresses { get; set; }
        public virtual DbSet<CustomerFamilyAndNomineeDetailsEntity> CustomerFamilyAndNominees { get; set; }
        public virtual DbSet<AccountsEntity> Accounts { get; set; }
        public virtual DbSet<AccountTypeEntity> AccountTypes { get; set; }
        public virtual DbSet<BranchEntity> Branches { get; set; }
        public virtual DbSet<TransactionsEntity> Transactions { get; set; }
        public virtual DbSet<DepositRatesEntity> DepositRates { get; set; }
        public virtual DbSet<LoginCredentialsEntity> LoginCredentials { get; set; }
        public virtual DbSet<KYCEntity> KYC {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasicDetailsEntity>().HasOne(a => a.CustomerAddress).WithOne(a => a.BasicDetails).HasForeignKey<CustomerAddressEntity>(a => a.CustomerId);
            modelBuilder.Entity<BasicDetailsEntity>().HasOne(a => a.CustomerFamilyAndNomineeDetails).WithOne(a => a.BasicDetails).HasForeignKey<CustomerFamilyAndNomineeDetailsEntity>(a => a.CustomerId);
            modelBuilder.Entity<BasicDetailsEntity>().HasOne(a => a.LoginCredentials).WithOne(a => a.BasicDetails).HasForeignKey<LoginCredentialsEntity>(a => a.CustomerId);
            modelBuilder.Entity<BasicDetailsEntity>().HasOne(a => a.KYC).WithOne(a => a.BasicDetails).HasForeignKey<KYCEntity>(a => a.CustomerId);
            modelBuilder.Entity<AccountsEntity>().HasOne(a => a.BasicDetails).WithMany(a => a.Accounts).HasForeignKey(a => a.CustomerId);
            modelBuilder.Entity<AccountsEntity>().HasOne(a => a.AccountTypes).WithMany(a => a.Accounts).HasForeignKey(a => a.AccountTypeId);
            modelBuilder.Entity<AccountsEntity>().HasOne(a => a.Branches).WithMany(a => a.Accounts).HasForeignKey(a => a.BranchID);
            modelBuilder.Entity<TransactionsEntity>().HasOne(a => a.SenderAccount).WithMany(a => a.SenderTransactions).HasForeignKey(a => a.SenderAccountNumber);
            modelBuilder.Entity<TransactionsEntity>().HasOne(a => a.ReceiverAcccount).WithMany(a => a.ReceiverTransactions).HasForeignKey(a=> a.ReceiverAccountNumber);
            base.OnModelCreating(modelBuilder);
        }
    }
}
