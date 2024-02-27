using Common_Layer.RequestModel;
using Manager_Layer.Interface;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            return repository.UserRegistration(model);
        }

        public string UserLogin(LoginModel model)
        {
            return repository.UserLogin(model);
        }

        public string GenerateToken(string Email, int UserId)
        {
            return repository.GenerateToken(Email, UserId);
        }

        public ForgetPasswordModel ForgetPassword(string Email)
        {
            return repository.ForgetPassword(Email);
        }

        public bool CheckUser(string Email)
        {
            return repository.CheckUser(Email);
        }

        public UserEntity ResetPassword(string Email, ResetModel model)
        {
            return repository.ResetPassword(Email, model);
        }



    }
}
