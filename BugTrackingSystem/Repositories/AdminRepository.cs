using BugTrackingSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.Repositories
{
    public class AdminRepository(string _connectionString)
    {
        //Add new user
        public void AddUser(string username, string password, string role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                connection.Execute(sql, new { Username = username, Password = password, Role = role });
            }
        }
        //Update user
        public void UpdateUser(int userId, string username, string password, string role)
        {
            var sql = @"UPDATE Users SET Username = @Username, Password = @Password, Role = @Role WHERE UserId = @UserId";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { UserId = userId, Username = username, Password = password, Role = role });
            }
        }
        //Delete user
        public void DeleteUser(int userId)
        {
            var sql = @"DELETE FROM Users WHERE UserId = @UserId";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { UserId = userId });
            }
        }
        //Get user by id
        public User GetUserById(int id)
        {
            var sql = @"SELECT * FROM Users WHERE UserId = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<User>(sql, new {Id = id});
            }
        }
        //See all users
        public IEnumerable<User> ViewAllUsers()
        {
            var sql = @"SELECT * FROM Users";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<User>(sql);
            }
        }
        //Get All Users By Role
        public IEnumerable<User> GetUsersByRole(string role)
        {
            var sql = @"SELECT * FROM Users WHERE Role = @Role";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<User>(sql, new { Role = role.ToLower()});
            }
        }

        //Add Project
        public void AddProject(string projectName)
        {
            var sql = @"INSERT INTO Projects (ProjectName) VALUES (@ProjectName)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { ProjectName = projectName });
            }
        }
        //See all projects
        public IEnumerable<Project> ViewAllProjects()
        {
            var sql = @"SELECT * FROM Projects";
            using (var connection = new SqlConnection(_connectionString))
            {
               return connection.Query<Project>(sql);
            }
        }
        //See all bugs
        public IEnumerable<Bug> ViewAllBugs()
        {
            var sql = @"SELECT * FROM Bugs";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Bug>(sql);
            }
        }
        //Get specifc user
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var sql = @"SELECT UserId, Username, Role FROM Users WHERE Username = @Username AND Password = @Password";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<User>(sql, new { Username = username, Password = password });
            }
        }
  

    }
}
