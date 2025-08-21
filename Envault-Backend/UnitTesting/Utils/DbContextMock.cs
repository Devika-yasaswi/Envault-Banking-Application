using CoreModels.Entities;
using DataAccessLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Utils
{
    public class DbContextMock
    {
        public Mock<ApplicationDBContext> _mockContext;
        public LoginAndRegistrationRepository _loginAndRegistrationRepository;
        public AccountOpeningRepository _accountOpeningRepository;
        public UserValidationRepository _userValidationRepository;
        public CustomerRepository _customerRepository;
        public AdminRepository _adminRepository;
        public LoanRepository _loanRepository;
        public IQueryable<LoginCredentialsEntity> _loginCredentials;
        public IQueryable<BasicDetailsEntity> _basicDetails;
        public IQueryable<CustomerAddressEntity> _customerAddress;
        public IQueryable<CustomerFamilyAndNomineeDetailsEntity> _customerFamilyAndNomineeDetails;
        public IQueryable<KYCEntity> _customerKYCs;
        public IQueryable<AccountsEntity> _accounts;
        public IQueryable<AccountTypeEntity> _accountTypes;
        public IQueryable<BranchEntity> _branches;
        public IQueryable<TransactionsEntity> _transactions;
        [OneTimeSetUp]
        public void Setup()
        {
            _loginCredentials = DataIntializer.GetLoginCredentials();
            _basicDetails = DataIntializer.GetBasicDetails();
            _customerAddress = DataIntializer.GetCustomerAddresses();
            _customerFamilyAndNomineeDetails = DataIntializer.GetCustomerFamilyAndNomineeDetails();
            _customerKYCs = DataIntializer.GetKYCs();
            _accounts = DataIntializer.GetAccounts();
            _accountTypes = DataIntializer.GetAccountTypes();
            _branches = DataIntializer.GetAllBranches();
            _transactions = DataIntializer.GetAllTransactions();
        }
        [SetUp]
        public void ReintializeTest()
        {
            _mockContext = new Mock<ApplicationDBContext>();

            var mockLoginSet = CreateMockDbSet(_loginCredentials);
            var mockBasicDetailsSet = CreateMockDbSet(_basicDetails);
            var mockCustomerAddressSet = CreateMockDbSet(_customerAddress);
            var mockCustomerFamilyAndNomineeDetailsSet = CreateMockDbSet(_customerFamilyAndNomineeDetails);
            var mockCustomerKYCsSet = CreateMockDbSet(_customerKYCs);
            var mockAccountsSet = CreateMockDbSet(_accounts);
            var mockAccountTypesSet  = CreateMockDbSet(_accountTypes);
            var mockBranchesSet = CreateMockDbSet(_branches);
            var mockTransactionSet = CreateMockDbSet(_transactions);

            _mockContext.Setup(c => c.Set<LoginCredentialsEntity>()).Returns(mockLoginSet.Object);
            _mockContext.Setup(c => c.Set<BasicDetailsEntity>()).Returns(mockBasicDetailsSet.Object);
            _mockContext.Setup(c => c.Set<CustomerAddressEntity>()).Returns(mockCustomerAddressSet.Object);
            _mockContext.Setup(c => c.Set<CustomerFamilyAndNomineeDetailsEntity>()).Returns(mockCustomerFamilyAndNomineeDetailsSet.Object);
            _mockContext.Setup(c => c.Set<KYCEntity>()).Returns(mockCustomerKYCsSet.Object);
            _mockContext.Setup(c => c.Set<AccountsEntity>()).Returns(mockAccountsSet.Object);
            _mockContext.Setup(c => c.Set<AccountTypeEntity>()).Returns(mockAccountTypesSet.Object);
            _mockContext.Setup(c => c.Set<BranchEntity>()).Returns(mockBranchesSet.Object);
            _mockContext.Setup(c => c.Set<TransactionsEntity>()).Returns(mockTransactionSet.Object);

            _loginAndRegistrationRepository = new LoginAndRegistrationRepository(_mockContext.Object);
            _accountOpeningRepository = new AccountOpeningRepository(_mockContext.Object);
            _userValidationRepository = new UserValidationRepository(_mockContext.Object);
            _adminRepository = new AdminRepository(_mockContext.Object);
            _customerRepository = new CustomerRepository(_mockContext.Object);
            _loanRepository = new LoanRepository(_mockContext.Object);
        }
        private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
