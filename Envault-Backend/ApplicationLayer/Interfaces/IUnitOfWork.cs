using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountOpeningRepository AccountOpeningRepository { get; }
        ILoginAndRegistrationRepository LoginAndRegistrationRepository { get; }
        IUserValidationRepository UserValidationRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IAdminRepository AdminRepository { get; }
    }
}
