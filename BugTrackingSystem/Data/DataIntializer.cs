using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.Data
{
    public class DataIntializer(string _connectionString)
    {
        public void Intialize()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create tables if they don't exist
                CreateTables(connection);

                // Seed with initial data if tables are empty
                SeedData(connection);
            }
        }

        private void CreateTables(SqlConnection connection)
        {
            var createTablesSql = @"
                IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL
                BEGIN
                    PRINT 'Users table already exists.'
                END
                ELSE
                BEGIN
                    CREATE TABLE Users (
                        UserId INT PRIMARY KEY IDENTITY,
                        Username NVARCHAR(50) NOT NULL,
                        Password NVARCHAR(50) NOT NULL,
                        Role NVARCHAR(20) NOT NULL
                    );
                END
                
                IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL
                BEGIN
                    PRINT 'Projects table already exists.'
                END
                ELSE
                BEGIN
                    CREATE TABLE Projects (
                        ProjectId INT PRIMARY KEY IDENTITY,
                        ProjectName NVARCHAR(100) NOT NULL
                    );
                END
                
                IF OBJECT_ID('dbo.Bugs', 'U') IS NOT NULL
                BEGIN
                    PRINT 'Bugs table already exists.'
                END
                ELSE
                BEGIN
                    CREATE TABLE Bugs (
                        BugId INT PRIMARY KEY IDENTITY,
                        BugName NVARCHAR(100) NOT NULL,
                        BugType NVARCHAR(50),
                        Priority NVARCHAR(20),
                        ProjectId INT FOREIGN KEY REFERENCES Projects(ProjectId),
                        BugDate DATETIME,
                        Status NVARCHAR(20),
                        DeveloperId INT FOREIGN KEY REFERENCES Users(UserId),
                        TesterId INT FOREIGN KEY REFERENCES Users(UserId)
                    );
                END
                 
                IF OBJECT_ID('dbo.BugLogs', 'U') IS NOT NULL
                BEGIN
                    PRINT 'BugLogs table already exists.'
                END
                ELSE
                BEGIN
                    CREATE TABLE BugLogs (
                        BugLogId INT PRIMARY KEY IDENTITY,
                        BugId INT FOREIGN KEY REFERENCES Bugs(BugId),
                        DeveloperId INT FOREIGN KEY REFERENCES Users(UserId),
                        TimeSpent INT 
                    );
                END
            ";

            connection.Execute(createTablesSql);
        }

        private void SeedData(SqlConnection connection)
        {
            var hasUsers = connection.QuerySingle<int>("SELECT COUNT(*) FROM Users") > 0;
            var hasProjects = connection.QuerySingle<int>("SELECT COUNT(*) FROM Projects") > 0;
            var hasBugs = connection.QuerySingle<int>("SELECT COUNT(*) FROM Bugs") > 0;

            if (hasUsers && hasProjects && hasBugs)
            {
                return;
            }

            var seedDataSql = @"
                INSERT INTO Users (Username, Password, Role) VALUES ('tester1', 'password1', 'tester');
                INSERT INTO Users (Username, Password, Role) VALUES ('developer1', 'password1', 'developer');
                INSERT INTO Users (Username, Password, Role) VALUES ('pm1', 'password1', 'pm');
                INSERT INTO Users (Username, Password, Role) VALUES ('admin1', 'password1', 'admin');
                INSERT INTO Users (Username, Password, Role) VALUES ('tester2', 'password2', 'tester');
                INSERT INTO Users (Username, Password, Role) VALUES ('developer2', 'password2', 'developer');
                INSERT INTO Users (Username, Password, Role) VALUES ('pm2', 'password2', 'pm');
                INSERT INTO Users (Username, Password, Role) VALUES ('admin2', 'password2', 'admin');
                INSERT INTO Projects (ProjectName) VALUES ('Project A');
                INSERT INTO Projects (ProjectName) VALUES ('Project B');
                INSERT INTO Projects (ProjectName) VALUES ('Project C');
                INSERT INTO Projects (ProjectName) VALUES ('Project D');
                INSERT INTO Bugs (BugName, BugType, Priority, ProjectId, BugDate, Status, DeveloperId, TesterId) VALUES 
                ('Bug 1', 'Type 1', 'High', 1, GETDATE(), 'Open', 2, 1),
                ('Bug 2', 'Type 2', 'Critical', 2, GETDATE(), 'Open', 2, 1),
                ('Bug 3', 'Type 3', 'Low', 1, GETDATE(), 'Open', 3, 1),
                ('Bug 4', 'Type 4', 'High', 2, GETDATE(), 'Closed', 6, 5),
                ('Bug 5', 'Type 5', 'Medium', 2, GETDATE(), 'Closed', 6, 5),
                ('Bug 6', 'Type 6', 'Critical', 1, GETDATE(), 'Open', 6, 5);
                INSERT INTO BugLogs (BugId, DeveloperId, TimeSpent) VALUES 
                (1, 2, 120),
                (2, 2, 90),
                (3, 2, 180),
                (4, 6, 75),
                (5, 6, 130),
                (6, 6, 150);
            ";

            connection.Execute(seedDataSql);
        }
}
}
