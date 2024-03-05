using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common_Layer.RequestModel
{
    public class RegisterModel
    {
        [RegularExpression("^[A-Z][a-z]{2,}",ErrorMessage = "Your input should start from caps with a min length 3")]
        public string FName { get; set; }
        [MaxLength(9,ErrorMessage = "MAx Length should be 9")]
        public string LName { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Provide a valid Mail.")]
        public string Email { get; set; }
        [DataType(DataType.Password,ErrorMessage = "Password should be strong")]
        public string Password { get; set; }

    }
}
