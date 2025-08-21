using CoreModels.Entities;
using DataAccessLayer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Utils
{
    public class DataIntializer
    {
        public static IQueryable<LoginCredentialsEntity> GetLoginCredentials()
        {
            IQueryable<LoginCredentialsEntity> loginCredentials = new List<LoginCredentialsEntity>
            {
                new() {CustomerId = 10000001 , CustomerPassword = "ABC@123k", SecurityMessage= "Welcome"},
                new() {CustomerId = 10000002 , CustomerPassword = "XYC@12uik", SecurityMessage= "Hello"},
                new() {CustomerId = 10000003 , CustomerPassword = "PGee@123r", SecurityMessage= "February"},
                new() {CustomerId = 10000004 , CustomerPassword = "PGee@123r", SecurityMessage= "February"}
            }.AsQueryable();
            return loginCredentials;
        }
        public static IQueryable<BasicDetailsEntity> GetBasicDetails()
        {
            IQueryable<BasicDetailsEntity> basicDetails = new List<BasicDetailsEntity>
            {
                new() {CustomerId = 10000001, CustomerName = "Sri Ram", DateOfBirth = DateOnly.Parse("1978-01-01"), Gender = "Male", AadharNumber = 981234567890, MobileNumber = 9876543210, PANNumber = "MKTPK0515M", CustomerEmail = "sriram@gmail.com", EmployementType = "Private Sector", AnnualIncome = 123456, Nationality = true, TaxResidentOfIndia = true},
                new() {CustomerId = 10000002, CustomerName = "Deepak", DateOfBirth = DateOnly.Parse("2007-02-21"), Gender = "Male", AadharNumber = 456777642837, MobileNumber = 9876763490, PANNumber = "ASDFK9124L", CustomerEmail = "deepak@gmail.com", EmployementType = "Public Sector", AnnualIncome = 9876543, Nationality = true, TaxResidentOfIndia =true},
                new() {CustomerId = 10000003, CustomerName = "Anjali", DateOfBirth = DateOnly.Parse("1977-02-12"), Gender = "Female", AadharNumber = 876876755649, MobileNumber = 7684638383, PANNumber = "CVBGH1237K", CustomerEmail = "anjali@gmail.com", EmployementType = "Private Sector", AnnualIncome = 200000, Nationality = true, TaxResidentOfIndia = true},
                new() {CustomerId = 10000004, CustomerName = "Srinivasa Rao", DateOfBirth = DateOnly.Parse("1967-02-12"), Gender = "Male", AadharNumber = 976876755649, MobileNumber = 9684638383, PANNumber = "HVBGH1237K", CustomerEmail = "srinivas@gmail.com", EmployementType = "Private Sector", AnnualIncome = 200000, Nationality = true, TaxResidentOfIndia = true}
            }.AsQueryable();
            return basicDetails;
        }
        public static IQueryable<CustomerAddressEntity> GetCustomerAddresses()
        {
            IQueryable<CustomerAddressEntity> customerAddresses = new List<CustomerAddressEntity>
            {
                new() {CustomerId = 10000001, PermanentHouseNo = "2/89", PermanentStreet = "Vidyanagar 1st line", PermanentCity = "Singarayakonda", PermanentState = "Andhra Pradesh", PermanentPincode = 897656, PresentHouseNo = "2/89", PresentStreet = "Vidyanagar 1st line", PresentCity = "Singarayakonda", PresentState = "Andhra Pradesh", PresentPincode = 897656 },
                new() {CustomerId = 10000002, PermanentHouseNo = "#25", PermanentStreet = "Arundalpet", PermanentCity = "Narasaraopet", PermanentState = "Andhra Pradesh", PermanentPincode = 678998, PresentHouseNo = "#25", PresentStreet = "Arundalpet", PresentCity = "Narasaraopet", PresentState = "Andhra Pradesh", PresentPincode = 678998 },
                new() {CustomerId = 10000003, PermanentHouseNo = "3-25", PermanentStreet = "Vidyanagar 7th line", PermanentCity = "Singarayakonda", PermanentState = "Andhra Pradesh", PermanentPincode = 897656, PresentHouseNo = "3-25", PresentStreet = "Vidyanagar 7th line", PresentCity = "Singarayakonda", PresentState = "Andhra Pradesh", PresentPincode = 897656 },
                new() {CustomerId = 10000004, PermanentHouseNo = "3-25", PermanentStreet = "Vidyanagar 7th line", PermanentCity = "Singarayakonda", PermanentState = "Andhra Pradesh", PermanentPincode = 897656, PresentHouseNo = "3-25", PresentStreet = "Vidyanagar 7th line", PresentCity = "Singarayakonda", PresentState = "Andhra Pradesh", PresentPincode = 897656 }
            }.AsQueryable();
            return customerAddresses;
        }
        public static IQueryable<CustomerFamilyAndNomineeDetailsEntity> GetCustomerFamilyAndNomineeDetails()
        {
            IQueryable<CustomerFamilyAndNomineeDetailsEntity> customerFamilyAndNomineeDetails = new List<CustomerFamilyAndNomineeDetailsEntity>
            {
                new() {CustomerId = 10000001, FatherName = "Dasaradh", MotherName = "Kousalya", SpouseName = "Seetha", NomineeName = "Dasaradh", NomineeDateOfBirth = DateOnly.Parse("1947-08-15"), RelationWithNominee = "Father", NomineeHouseNo = "3-25", NomineeStreet = "Vidyanagar 7th line", NomineeCity = "Singarayakonda", NomineeState = "Andhra Pradesh", NomineePincode = 897656},
                new() {CustomerId = 10000002, FatherName = "K V S Koteswara Rao", MotherName = "Anjali", NomineeName = "K V S Koteswara Rao", NomineeDateOfBirth = DateOnly.Parse("1956-09-08"), RelationWithNominee = "Father", NomineeHouseNo = "#25", NomineeStreet = "Arundalpet", NomineeCity = "Narasaraopet", NomineeState = "Andhra Pradesh", NomineePincode = 678998},
                new() {CustomerId = 10000003, FatherName = "Jaganadham", MotherName = "Annapurnamma", SpouseName = "K V S Koteswara Rao", NomineeName = "K V S Koteswara Rao", NomineeDateOfBirth = DateOnly.Parse("1979-07-19"), RelationWithNominee = "Spouse",NomineeHouseNo = "3-25", NomineeStreet = "Vidyanagar 7th line", NomineeCity = "Singarayakonda", NomineeState = "Andhra Pradesh", NomineePincode = 897656},
                new() {CustomerId = 10000004, FatherName = "Jaganadham", MotherName = "Annapurnamma", SpouseName = "K V S Koteswara Rao", NomineeName = "K V S Koteswara Rao", NomineeDateOfBirth = DateOnly.Parse("1979-07-19"), RelationWithNominee = "Spouse",NomineeHouseNo = "3-25", NomineeStreet = "Vidyanagar 7th line", NomineeCity = "Singarayakonda", NomineeState = "Andhra Pradesh", NomineePincode = 897656}
            }.AsQueryable();
            return customerFamilyAndNomineeDetails;
        }
        public static IQueryable<KYCEntity> GetKYCs()
        {
            IQueryable<KYCEntity> customerKYCs = new List<KYCEntity>
            {
                new() {CustomerId = 10000001, KYCStatus = "approved" },
                new() {CustomerId = 10000002, KYCStatus = "approved"},
                new() {CustomerId = 10000003, KYCStatus = "rejected"}
            }.AsQueryable();
            return customerKYCs;
        }
        public static IQueryable<AccountsEntity> GetAccounts()
        {
            IQueryable<AccountsEntity> customerAccounts = new List<AccountsEntity>
            {
                new() {AccountNumber = 100000000001, CustomerId = 10000001, BranchID = 1, AccountBalance = 0.02, AccountTypeId = 1, FreezeWithdrawl = false, MaxBalanceWarningAmount = 100, MinBalanceWarningAmount = 0.1},
                new() {AccountNumber = 100000000002, CustomerId = 10000002, BranchID = 2, AccountBalance = 1000, AccountTypeId = 2, FreezeWithdrawl = true, MaxBalanceWarningAmount = 2000, MinBalanceWarningAmount = 100},
                new() {AccountNumber = 100000000003, CustomerId = 10000003, BranchID = 3, AccountBalance = 0, AccountTypeId = 3, FreezeWithdrawl = false, MaxBalanceWarningAmount = 0, MinBalanceWarningAmount =0 },
                new() {AccountNumber = 100000000004, CustomerId = 10000004, BranchID = 3, AccountBalance = 0, AccountTypeId = 3, FreezeWithdrawl = false, MaxBalanceWarningAmount = 0, MinBalanceWarningAmount =0 }
            }.AsQueryable();
            return customerAccounts;
        }
        public static IQueryable<AccountTypeEntity> GetAccountTypes()
        {
            IQueryable<AccountTypeEntity> accountTypes = new List<AccountTypeEntity>
            {
                new() {AccountTypeId = 1, AccountType = "Easy Access Savings Account" },
                new() {AccountTypeId = 2, AccountType = "Amaze Savings Account"},
                new() {AccountTypeId = 3, AccountType = "Liberty Savings Account"}
            }.AsQueryable();
            return accountTypes;
        }
        public static IQueryable<BranchEntity> GetAllBranches()
        {
            IQueryable<BranchEntity> branches = new List<BranchEntity>
            {
                new() {BranchID = 1, BranchLocatedState = "Andhra Pradesh", City ="Visakhapatnam", BranchAddress = "123 Beach Road, Near RK Beach", IFSCCode = "ENV0001234"},
                new() {BranchID = 2, BranchLocatedState = "Andhra Pradesh", City = "Visakhapatnam", BranchAddress = "456 Beach Road, Near RK Beach", IFSCCode = "ENV0005678"},
                new() {BranchID = 3, BranchLocatedState = "Andhra Pradesh", City = "Vijayawada", BranchAddress = "789 MG Road, Near Benz Circle", IFSCCode = "ENV0003456"}
            }.AsQueryable();
            return branches;
        }
        public static IQueryable<TransactionsEntity> GetAllTransactions()
        {
            IQueryable<TransactionsEntity> transactions = new List<TransactionsEntity>
            {
                new() {TransactionID = 1000000, SenderAccountNumber = 100000000001, ReceiverAccountNumber =100000000001, Amount = 1000, TransactionTime = DateTime.Parse("2025-02-27 08:22:14.5384101"), Remarks = "Amount Deposit by self"},
                new() {TransactionID = 1000001, SenderAccountNumber = 100000000001, ReceiverAccountNumber =100000000002, Amount = 1000, TransactionTime = DateTime.Parse("2025-02-27 08:23:31.7628728"), Remarks ="Transferring to my self account"},
                new() {TransactionID = 1000002, SenderAccountNumber = 100000000001, ReceiverAccountNumber = 100000000001, Amount = 0.02, TransactionTime = DateTime.Parse("2025-02-27 09:07:50.8597844"), Remarks = "Amount Deposit by self"}
            }.AsQueryable();
            return transactions;
        }
    }
}
