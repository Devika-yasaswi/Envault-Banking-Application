using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class UserValidationRepository : IUserValidationRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private static readonly Random _random = new Random();
        public UserValidationRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Generates OTP to validate user
        public int GenerateOtp()
        {
            try
            {
                var otp = _random.Next(100000, 999999);
                return otp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Validates OTP entered by the user
        public bool ValidateOtp(int generatedOtp, int userEnteredOtp)
        {
            try
            {
                if (generatedOtp == userEnteredOtp)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Generates CAPTCHA for validating user
        public string GenerateCaptcha()
        {
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new();
                char[] code = new char[6];
                for (int i = 0; i < 6; i++)
                {
                    code[i] = chars[random.Next(chars.Length)];
                }
                return new string(code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Validates CAPTCHA entered by the user
        public bool ValidateCaptcha(string generatedCaptcha, string userEnteredCaptcha)
        {
            try
            {
                if (generatedCaptcha == userEnteredCaptcha)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}