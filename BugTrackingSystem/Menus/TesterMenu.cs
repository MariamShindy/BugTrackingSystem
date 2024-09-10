using BugTrackingSystem.Models;
using BugTrackingSystem.Repositories;
using BugTrackingSystem.Services;
using System;
using System.Linq;

namespace BugTrackingSystem.Menus
{
    public class TesterMenu(TesterRepository _testerRepo , UserService _userService)
    {
        public void ShowMenu(User currentUser)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("*************************************");
                Console.WriteLine("Tester Module");
                Console.WriteLine("*************************************");
                Console.ResetColor();
                Console.WriteLine("1. Create Bug");
                Console.WriteLine("2. Assign Bug to Developer");
                Console.WriteLine("3. View Bugs");
                Console.WriteLine("4. Logout");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateBug(currentUser);
                        break;
                    case "2":
                        AssignBug();
                        break;
                    case "3":
                        ViewBugs();
                        break;
                    case "4":
                        return;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option. Try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void CreateBug(User tester)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************************************");
            Console.WriteLine("Create Bug");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            Console.Write("Bug Name: ");
            var bugName = Console.ReadLine();
            Console.Write("Bug Type: ");
            var bugType = Console.ReadLine();
            Console.Write("Priority: ");
            var priority = Console.ReadLine();
            Console.Write("Project ID: ");
            int projectId;
            do
            {
                Console.Write("Please Enter Valid Project ID: ");
            } while (!int.TryParse(Console.ReadLine(), out projectId) || !_userService.IsValidProject(projectId));
            Console.Write("Bug Date (yyyy-MM-dd): ");
            var bugDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Status (Open/Closed): ");
            var status = Console.ReadLine();

            _testerRepo.CreateBug(bugName, bugType, priority, projectId, bugDate, status, tester.UserId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bug created successfully.");
            Console.ResetColor();
        }

        private void AssignBug()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*************************************");
            Console.WriteLine("Assign Bug to Developer");
            Console.WriteLine("*************************************");
            Console.ResetColor();

            Console.WriteLine("Select a bug to assign:");
            var bugs = _testerRepo.ViewBugs(false); 
            if (!bugs.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No open bugs available.");
                Console.ResetColor();
                return;
            }

            foreach (var bug in bugs)
            {
                Console.WriteLine($"{bug.BugId}: {bug.BugName} - {bug.Status}");
            }

            Console.Write("Enter Bug ID to assign: ");
            if (!int.TryParse(Console.ReadLine(), out var bugId) || bugs.All(b => b.BugId != bugId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Bug ID.");
                Console.ResetColor();
                return;
            }
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


            _testerRepo.AssignBug(bugId, developerId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bug assigned successfully.");
            Console.ResetColor();
        }
        private void ViewBugs()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*************************************");
            Console.WriteLine("View Bugs");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            Console.WriteLine("Show closed bugs? (yes/no)");
            var showClosed = Console.ReadLine()?.Trim().ToLower() == "yes";

            var bugs = _testerRepo.ViewBugs(showClosed);
            foreach (var bug in bugs)
            {
                Console.WriteLine($"{bug.BugId}: {bug.BugName} - {bug.Status}");
            }
        }
    }
}
