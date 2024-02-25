using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserRepository:IUserRepository
    {
        private readonly FundooContext context;

        public UserRepository(FundooContext context)
        {
            this.context = context;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            if (context.UserTable.FirstOrDefault(a => a.Email == model.Email) == null)
            {
                UserEntity entity = new UserEntity();
                entity.FName = model.FName;
                entity.LName = model.LName;
                entity.Email = model.Email;
                entity.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                context.UserTable.Add(entity);
                context.SaveChanges();
                return entity;
            }
            else
            {
                throw new Exception("User Already Exist.");
            }
        }
    }
}
