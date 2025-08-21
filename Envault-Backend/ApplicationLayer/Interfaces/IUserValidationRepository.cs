using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IUserValidationRepository
    {
        int GenerateOtp();
        bool ValidateOtp(int generatedOtp, int userEnteredOtp);
        string GenerateCaptcha();
        bool ValidateCaptcha(string generatedCaptcha, string userEnteredCaptcha);
    }
}
