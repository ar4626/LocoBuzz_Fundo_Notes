using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.RequestModel
{
    public class ForgetPasswordModel
    {
        public string Email { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }

    }
}
