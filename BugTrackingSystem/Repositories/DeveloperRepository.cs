using BugTrackingSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BugTrackingSystem.Repositories
{
    public class DeveloperRepository(string _connectionString)
    {

        //View assigned bugs to specific developer
        public IEnumerable<Bug> ViewAssignedBugs(int developerId)
        {
            var sql = @"SELECT * FROM Bugs WHERE DeveloperId = @DeveloperId";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Bug>(sql, new { DeveloperId = developerId });

            }
        }
        //Change bug status
        public void ChangeBugStatus(int bugId, string newStatus)
        {
            var sql = @"UPDATE Bugs SET Status = @NewStatus WHERE BugId = @BugId";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { BugId = bugId, NewStatus = newStatus });

            }
        }

    }
}
