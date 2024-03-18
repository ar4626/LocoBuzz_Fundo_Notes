using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class ReviewRepository : IReviewRepository
    {
        FundooContext context;

        public ReviewRepository(FundooContext context)
        {
            this.context = context;
        }

        public UserEntity updateFnameLname(string email, string fname, string lname)
        {
            var user = context.UserTable.FirstOrDefault(a=>a.Email == email);
            if(user != null)
            {
                user.FName = fname;
                user.LName = lname;
                context.SaveChanges();
                return user;
            }
            else
            {
                return null;
            }
        }

        public List<UserEntity> showUser (string fname)
        {
            List<UserEntity> user = context.UserTable.Where(a=>a.FName == fname).ToList();  
            return user;
        }

        public int countUser()
        {
            return context.UserTable.Count();
        }
    }
}
