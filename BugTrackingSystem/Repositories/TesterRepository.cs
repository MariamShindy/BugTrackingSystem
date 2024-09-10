using BugTrackingSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BugTrackingSystem.Repositories
{
    public class TesterRepository(string _connectionString)
    {
        //Create bug
        public void CreateBug(string bugName, string bugType, string priority, int projectId, DateTime bugDate, string status, int testerId)
        {
            var sql = @"INSERT INTO Bugs (BugName, BugType, Priority, ProjectId, BugDate, Status, TesterId)
                    VALUES (@BugName, @BugType, @Priority, @ProjectId, @BugDate, @Status, @TesterId)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { BugName = bugName, BugType = bugType, Priority = priority, ProjectId = projectId, BugDate = bugDate, Status = status, TesterId = testerId });
            }
        }

        //Assign bug to developer
        public void AssignBug(int bugId, int developerId)
        {
            var sql = @"UPDATE Bugs SET DeveloperId = @DeveloperId WHERE BugId = @BugId";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { BugId = bugId, DeveloperId = developerId });
            }
        }

        //View bugs
        public IEnumerable<Bug> ViewBugs(bool showClosed)
        {
            var sql = @"SELECT * FROM Bugs WHERE Status = @Status OR @Status = 'All'";
            var status = showClosed ? "Closed" : "Open";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Bug>(sql, new { Status = status });
            }
        }
    }
}
