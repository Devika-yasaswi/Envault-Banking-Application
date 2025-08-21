using ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserValidationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserValidationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public int GenerateOtp()
        {
            return _unitOfWork.UserValidationRepository.GenerateOtp();
        }
        public bool ValidateOtp(int generatedOtp, int userEnteredOtp)
        {
            return _unitOfWork.UserValidationRepository.ValidateOtp(generatedOtp, userEnteredOtp);
        }
        public string GenerateCaptcha()
        {
            return _unitOfWork.UserValidationRepository.GenerateCaptcha();
        }
        public bool ValidateCaptcha(string generatedCaptcha, string userEnteredCaptcha)
        {
            return _unitOfWork.UserValidationRepository.ValidateCaptcha(generatedCaptcha, userEnteredCaptcha);
        }
    }
}
