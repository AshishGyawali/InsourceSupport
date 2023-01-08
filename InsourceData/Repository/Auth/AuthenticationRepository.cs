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

        public async Task<LoginUserViewModel> GetCredentials(LoginViewModel credentials)
        {
            var query = @"SELECT [ID],[Username],[Email],[Password],[FullName],[ContactNumber],[JoinedDate],[RoleId],[IsSystemUser] from [User] WHERE [Username] = @Username OR [Email] = @Username;";
            var pram = new DynamicParameters();
            pram.Add("Username", credentials.UserName, DbType.String);
            return await _db.ExecuteObject<LoginUserViewModel>(query, CommandType.Text,pram);
        }
    }
}
