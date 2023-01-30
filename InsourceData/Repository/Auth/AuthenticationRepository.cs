 using Dapper;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models.ViewModel.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Repository.Auth
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly Database _db;
        public AuthenticationRepository(Database database)
        {
            _db = database;
        }

        public async Task<DbResponse> RegisterUser(RegisterUserViewModel details)
        {
            var query = @"insert into[User]([UserName],[Email],[Password],[Salt],[FullName],[ContactNumber],[JoinedDate],[RoleId],[IsSystemUser])VALUES(@UserName, @Email, @Password, @Salt, @FullName, @ContactNumber, @JoinedDate, @RoleId, @IsSystemUser);";
            var pram = new DynamicParameters()
            .AddParam("UserName", details.UserName)
            .AddParam("Email", details.Email)
            .AddParam("Password", details.Password)
            .AddParam("Salt", details.Salt)
            .AddParam("FullName", details.FullName)
            .AddParam("ContactNumber", details.ContactNumber)
            .AddParam("JoinedDate", details.JoinedDate)
            .AddParam("RoleId", details.RoleId)
            .AddParam("IsSystemUser", details.IsSystemUser);
            return await _db.ExecuteNonQueryAsync(query, CommandType.Text,pram);
        }

        public async Task<LoginUserViewModel> GetCredentials(LoginViewModel credentials)
        {
            var query = @"SELECT U.[ID],U.[Username],U.[Email],U.[Password],U.[Salt],U.[FullName],U.[ContactNumber],U.[JoinedDate],R.[Name] AS [Role],U.[IsSystemUser] from [User] U INNER JOIN [Role] R ON U.RoleId = R.Id WHERE [Username] = @Username OR [Email] = @Username;";
            var pram = new DynamicParameters();
            pram.Add("Username", credentials.UserName, DbType.String);
            return await _db.ExecuteObject<LoginUserViewModel>(query, CommandType.Text,pram);
        }
    }
}
