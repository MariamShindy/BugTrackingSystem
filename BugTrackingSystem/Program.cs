using BugTrackingSystem.Data;
using BugTrackingSystem.Menus;
using BugTrackingSystem.Models;
using BugTrackingSystem.Repositories;
using BugTrackingSystem.Services;
using BugTrackingSystem.Utilities;

namespace BugTrackingSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server = .; Database = BugsSystem; Trusted_Connection = true; Encrypt = False; TrustServerCertificate = True";
            var dataIntializer = new DataIntializer(connectionString);
            dataIntializer.Intialize();

            var adminRepo = new AdminRepository(connectionString);
            var developerRepo = new DeveloperRepository(connectionString);
            var projectManagerRepo = new ProjectManagerRepository(connectionString);
            var testerRepo = new TesterRepository(connectionString);
            var userService = new UserService(adminRepo, projectManagerRepo);
            while (true)
            {
                User loggedInUser = null!;
                while (loggedInUser == null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("*************************************");
                    Console.WriteLine("Login");
                    Console.WriteLine("*************************************");
                    Console.ResetColor();
                    Console.Write("Username: ");
                    var username = Console.ReadLine();
                    Console.Write("Password: ");
                    var password = Utility.ReadPassword();

                    loggedInUser = userService.AuthenticateUser(username, password);

                    if (loggedInUser == null || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Invalid credentials. Try again.");
                        Console.ResetColor();
                        Thread.Sleep(500);
                    }
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*************************************");
                Console.WriteLine($"Welcome, {loggedInUser.Username}!");
                Console.WriteLine("*************************************");
                Console.ResetColor();

                switch (loggedInUser.Role.ToLower())
                {
                    case "tester":
                        var testerMenu = new TesterMenu(testerRepo, userService);
                        testerMenu.ShowMenu(loggedInUser);
                        break;

                    case "developer":
                        var developerMenu = new DeveloperMenu(developerRepo);
                        developerMenu.ShowMenu(loggedInUser);
                        break;

                    case "pm":
                        var pmMenu = new ProjectManagerMenu(projectManagerRepo, adminRepo, userService);
                        pmMenu.ShowMenu(loggedInUser);
                        break;

                    case "admin":
                        var adminMenu = new AdminMenu(adminRepo, userService);
                        adminMenu.ShowMenu();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid role.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
