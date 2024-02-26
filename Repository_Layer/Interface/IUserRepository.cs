using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRepository
    {
        public UserEntity UserRegistration(RegisterModel model);
        public string UserLogin(LoginModel model);
    }
}
