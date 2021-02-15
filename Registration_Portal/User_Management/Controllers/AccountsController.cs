using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User_Management.Context;
using User_Management.Services;
using User_Management.ViewModels;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IDapper _dapper;
        private IConfiguration _configuration;
        private readonly MyContext _myContext;

        public AccountsController(IDapper dapper, IConfiguration configuration, MyContext myContext)
        {
            _dapper = dapper;
            _configuration = configuration;
            _myContext = myContext;
        }

        [HttpPost(nameof(RegisterUser))]
        public async Task<int> RegisterUser(GeneralVM data)
        {
            var password = $"{data.LastName}default";
            data.Password = BCrypt.Net.BCrypt.HashPassword(password);

            var dbparams = new DynamicParameters();
            // Employee Parameter
            dbparams.Add("FirstName", data.FirstName, DbType.String);
            dbparams.Add("LastName", data.LastName, DbType.String);
            dbparams.Add("BirthDate", data.BirthDate, DbType.Date);
            dbparams.Add("Gender", data.Gender, DbType.String);
            // User Parameter
            dbparams.Add("Email", data.Email, DbType.String);
            dbparams.Add("Username", data.Username, DbType.String);
            dbparams.Add("Phone", data.Phone, DbType.String);
            dbparams.Add("Password", data.Password, DbType.String);
            // User Religion
            dbparams.Add("ReligionID", data.ReligionId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_RegisterUser]", dbparams, commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpPost(nameof(Login))]
        public async Task<string> Login(GeneralVM data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("Email", data.Username, DbType.String);
            dbparams.Add("Username", data.Username, DbType.String);
            dbparams.Add("Phone", data.Username, DbType.String);

            var result = await Task.FromResult(_dapper.Get<GeneralVM>("[dbo].[SP_Login]", dbparams, commandType: CommandType.StoredProcedure));

            if (BCrypt.Net.BCrypt.Verify(data.Password, result.Password))
            {
                if (result.EducationId != 0)
                {
                    var claim = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", $"{result.UserId}"),
                        new Claim("EmployeeId", $"{result.EmployeeId}"),
                        new Claim("ReligionId", $"{result.ReligionId}"),
                        new Claim("EducationId", $"{result.EducationId}"),
                        new Claim("UniversityId", $"{result.UniversityId}"),
                        new Claim("DepartmentId", $"{result.DepartmentId}"),
                        new Claim("DegreeId", $"{result.DegreeId}")
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claim, expires: DateTime.UtcNow.AddMinutes(5), signingCredentials: signIn);
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    var claim = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", $"{result.UserId}"),
                        new Claim("EmployeeId", $"{result.EmployeeId}"),
                        new Claim("ReligionId", $"{result.ReligionId}")
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claim, expires: DateTime.UtcNow.AddMinutes(5), signingCredentials: signIn);
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
            }
            else return null;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut(nameof(ChangePassword))]
        public async Task<int> ChangePassword(GeneralVM data)
        {
            var dbparams = new DynamicParameters();
            data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            dbparams.Add("@Email", data.Email, DbType.String);
            dbparams.Add("@Password", data.Password, DbType.String);
            var updateUser = await Task.FromResult(_dapper.Update<int>("[dbo].[SP_ChangePassword]",
                                dbparams,
                                commandType: CommandType.StoredProcedure));
            return updateUser;

        }

        [HttpPatch(nameof(ForgotPassword))]
        public async Task<int> ForgotPassword(GeneralVM data)
        {
            Guid id = Guid.NewGuid();
            string guid = id.ToString();
            var dbparams = new DynamicParameters();
            dbparams.Add("@Email", data.Email, DbType.String);
            dbparams.Add("@Password", BCrypt.Net.BCrypt.HashPassword(guid), DbType.String);

            var result = await Task.FromResult(_dapper.Update<int>("[dbo].[SP_ChangePassword]", dbparams,
            commandType: CommandType.StoredProcedure));

            MailMessage mm = new MailMessage("e2ftspen.ga@gmail.com", data.Email.ToString());
            mm.Subject = "Reset Your Password ! " + DateTime.Now.ToString();
            mm.Body = string.Format("Hello is your email " + data.Email.ToString() + " your password is " + guid);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential();
            nc.UserName = "e2ftspen.ga@gmail.com";
            nc.Password = "smpn3cilegon";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Port = 587;
            smtp.Send(mm);
            return result;
        }
    }
}
