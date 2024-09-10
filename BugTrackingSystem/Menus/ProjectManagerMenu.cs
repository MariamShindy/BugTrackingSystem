using BugTrackingSystem.Models;
using BugTrackingSystem.Repositories;
using BugTrackingSystem.Services;

namespace BugTrackingSystem.Menus
{
    public class ProjectManagerMenu(ProjectManagerRepository _pmRepo,AdminRepository _adminRepo , UserService _userService)
    {
        public void ShowMenu(User currentUser)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("*************************************");
                Console.WriteLine("Project Manager Module");
                Console.WriteLine("*************************************");
                Console.ResetColor();
                Console.WriteLine("1. Monitor Bugs");
                Console.WriteLine("2. Get Developer Hours");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        MonitorBugs();
                        break;
                    case "2":
                        GetDeveloperHours();
                        break;
                    case "3":
                        return;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private void MonitorBugs()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************************************");
            Console.WriteLine("Monitor Bugs");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            Console.Write("Project ID: ");
            int projectId; 
            do
            {
                Console.Write("Please Enter Valid Project ID: ");
            } while (!int.TryParse(Console.ReadLine(),out projectId) ||!_userService.IsValidProject(projectId));
           
            Console.Write("Show Closed Bugs? (yes/no): ");
            var showClosed = Console.ReadLine().ToLower() == "yes";

            var bugs = _pmRepo.MonitorBugs(projectId, showClosed);
            Console.WriteLine($"Bugs is not null {bugs.Count()}");
            foreach (var bug in bugs)
            {
                Console.WriteLine($"{bug.BugId}: {bug.BugName} - {bug.Status}");
            }
        }

        private void GetDeveloperHours()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************************************");
            Console.WriteLine("Get Developer Hours");
            Console.WriteLine("*************************************");
            Console.WriteLine("All Developers : ");
            var developers = _adminRepo.GetUsersByRole("developer");
            foreach(var developer in developers)
            {
                Console.WriteLine($"{developer.UserId} --> : username --> {developer.Username}, role--> {developer.Role}");
            }
            Console.ResetColor();
            int developerId;
            bool isValidInput;
            do
            {
                Console.Write("Enter Developer ID: ");
                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out developerId);

                if (!isValidInput || !_userService.IsValidUser(developerId,"developer"))
                {
                    Console.WriteLine("Invalid Developer ID. Please enter a valid Developer ID.");
                    isValidInput = false;
                }
            } while (!isValidInput);
            var bugLogs = _pmRepo.GetDeveloperHours(developerId);
            foreach (var log in bugLogs)
            {
                Console.WriteLine($"Bug ID: {log.BugId}, Time Spent: {log.TimeSpent} hours");
            }
        }
    }
}
