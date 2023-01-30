using Dapper;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models;
using InsourceData.Models.Enquiry;
using InsourceData.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;

namespace InsourceData.Repository
{
    public class EnquiryRepository : IEnquiryRepository
    {
        private readonly Database _db;

        public EnquiryRepository(Database database)
        {
            _db = database;
        }

        public Task<DbResponse> ProcessIssue(int id)
        {
            var query = @"Update Enquiry set TokenStatusId = 2 where id = @id;";
            var pram = new DynamicParameters();
            pram.Add("id", id, DbType.Int32);
            return _db.ExecuteNonQueryAsync(query,CommandType.Text,pram);
        }
        public Task<DbResponse> RejectIssue(int id)
        {
            var query = @"Update Enquiry set TokenStatusId = 4 where id = @id;";
            var pram = new DynamicParameters();
            pram.Add("id", id, DbType.Int32);
            return _db.ExecuteNonQueryAsync(query, CommandType.Text, pram);
        }

        public async Task<DbResponse> SaveEnquiry(EnquiryViewModel enquiry)
        {
            var query = @"INSERT INTO Enquiry(SoftwareId, ModuleId, Username, FullName, Email, ContactNumber, Issue, Files,DocumentNumber,Token,TokenStatusId)
                          VALUES(@SoftwareId, @ModuleId, @Username, @FullName, @Email, @ContactNumber, @Issue, @Files, dbo.fn_getDocumentNumber('Enquiry',1), dbo.fn_getDocumentNumber('Enquiry',0),1);";
            var pram = new DynamicParameters();
            //if (enquiry.Files != null)
            //{
            //    string folder = "images/issuePhoto/";
            //    folder += Guid.NewGuid().ToString() + "_" + enquiry.Files.FileName;
            //    enquiry.Files = folder;
            //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
            //    await employee.Photo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            //}

            pram.Add("SoftwareId", enquiry.SoftwareId, DbType.Int32);
            pram.Add("ModuleId", enquiry.ModuleId, DbType.Int32);
            pram.Add("Username", enquiry.UserName, DbType.String);
            pram.Add("Fullname", enquiry.FullName, DbType.String);
            pram.Add("Email", enquiry.Email, DbType.String);
            pram.Add("ContactNumber", enquiry.ContactNumber, DbType.String);
            pram.Add("Issue", enquiry.Issue, DbType.String);
            pram.Add("Files", enquiry.FilePath, DbType.String);
            return await _db.ExecuteNonQueryAsync(query,CommandType.Text,pram);
        }
       
    }
}
