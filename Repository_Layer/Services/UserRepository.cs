using Common_Layer.RequestModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
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
                _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}
