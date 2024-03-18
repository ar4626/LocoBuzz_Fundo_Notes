using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IReviewRepository
    {
        public UserEntity updateFnameLname(string email, string fname, string lname);
        public List<UserEntity> showUser(string fname);
        public int countUser();



    }
}
