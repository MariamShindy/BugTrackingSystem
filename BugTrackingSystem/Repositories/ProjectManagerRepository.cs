using BugTrackingSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BugTrackingSystem.Repositories
{
    public class ProjectManagerRepository(string _connectionString)
    {
        //Monitor the bugs
        public IEnumerable<Bug> MonitorBugs(int projectId, bool showClosed)
        {
            var sql = @"SELECT * FROM Bugs WHERE ProjectId = @ProjectId AND (Status = @Status OR @Status = 'All')";
            var status = showClosed ? "Closed" : "Open";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Bug>(sql, new { ProjectId = projectId, Status = status });
            }
        }
        //Get hours spent by developer in specific bug
        public IEnumerable<BugLog> GetDeveloperHours(int developerId)
        {
            var sql = @"SELECT * FROM BugLogs WHERE DeveloperId = @DeveloperId";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<BugLog>(sql, new { DeveloperId = developerId });
            }
        }


    }
}
