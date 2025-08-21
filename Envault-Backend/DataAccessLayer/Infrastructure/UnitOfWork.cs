using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountOpeningRepository AccountOpeningRepository { get; set; }
        public ILoginAndRegistrationRepository LoginAndRegistrationRepository { get; set; }
        public IUserValidationRepository UserValidationRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }

        public ApplicationDBContext _dbContext;
        public UnitOfWork(ApplicationDBContext dbContext, IAccountOpeningRepository accountOpeningRepository, ILoginAndRegistrationRepository loginAndRegistrationRepository, IUserValidationRepository userValidationRepository, ICustomerRepository customerRepository, IAdminRepository adminRepository) 
        {
            _dbContext = dbContext;
            AccountOpeningRepository = accountOpeningRepository;
            LoginAndRegistrationRepository = loginAndRegistrationRepository;
            UserValidationRepository = userValidationRepository;
            CustomerRepository = customerRepository;
            AdminRepository = adminRepository;
        }
    }
}
