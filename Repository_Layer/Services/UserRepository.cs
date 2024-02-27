using Common_Layer.RequestModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using Repository_Layer.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserRepository:IUserRepository
    {
        private readonly FundooContext context;
        private readonly IConfiguration _config;

        public UserRepository(FundooContext context, IConfiguration config)
        {
            this.context = context;
            this._config = config;
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

        public string UserLogin(LoginModel model)
        {

            var user = context.UserTable.FirstOrDefault(a => a.Email == model.Email);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                    string token = GenerateToken(user.Email, user.UserId);
                    return token;
                }
                else
                {
                    throw new Exception("Invalid Password!");
                }
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        public string GenerateToken(string Email, int UserId)
        {
            //Defining a Security Key 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        public ForgetPasswordModel ForgetPassword(string Email)
        {
            UserEntity User = context.UserTable.FirstOrDefault(x => x.Email == Email);
            ForgetPasswordModel forgetPassword = new ForgetPasswordModel();
            forgetPassword.Email = User.Email;
            forgetPassword.UserId = User.UserId;
            forgetPassword.Token = GenerateToken(Email,User.UserId);
            return forgetPassword;
        }

        public bool CheckUser(string Email)
        {
            if(context.UserTable.FirstOrDefault(a=>a.Email == Email) == null) return false;
            return true;
        }

        public bool ResetPassword(string Email, ResetModel model)
        {
            UserEntity user = context.UserTable.ToList().Find(x => x.Email == Email);   
            
            if(CheckUser(user.Email))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /*public bool VerifyResetToken(string Email, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            //Token Validation
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Issuer"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            return emailClaim == Email;
        }

        public UserEntity ResetPassword(string Email, ResetModel model)
        {
            if (VerifyResetToken(Email, model.Token))
            {
                var user = context.UserTable.FirstOrDefault(context => context.Email == Email);
                if (user != null)
                {
                    //user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                    user.Password = model.NewPassword;
                    context.SaveChanges();
                    return user;
                }
                return null;
            }
            else
            {
                throw new Exception("Invalid Token");
            }
        }*/
    }
}

