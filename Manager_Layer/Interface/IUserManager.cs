﻿using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interface
{
    public interface IUserManager
    {
        public UserEntity UserRegistration(RegisterModel model);

        public string UserLogin(LoginModel model);

        public string GenerateToken(string Email, int UserId);

        public ForgetPasswordModel ForgetPassword(string Email);
        public bool CheckUser(string Email);

        public UserEntity ResetPassword(string Email, ResetModel model);


    }
}
