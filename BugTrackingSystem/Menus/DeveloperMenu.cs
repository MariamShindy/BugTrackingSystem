using BugTrackingSystem.Models;
using BugTrackingSystem.Repositories;

namespace BugTrackingSystem.Menus
{
    public class DeveloperMenu(DeveloperRepository _developerRepo)
    {
        public void ShowMenu(User currentUser)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*************************************");
                Console.WriteLine("Developer Module");
                Console.WriteLine("*************************************");
                Console.ResetColor();
                Console.WriteLine("1. View Assigned Bugs");
                Console.WriteLine("2. Change Bug Status");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ViewAssignedBugs(currentUser);
                        break;
                    case "2":
                        ChangeBugStatus(currentUser);
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

        private void ViewAssignedBugs(User developer)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*************************************");
            Console.WriteLine("View Assigned Bugs");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            var bugs = _developerRepo.ViewAssignedBugs(developer.UserId);
            foreach (var bug in bugs)
            {
                Console.WriteLine($"{bug.BugId}: {bug.BugName} - {bug.Status}");
            }
        }

        private void ChangeBugStatus(User developer)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*************************************");
            Console.WriteLine("Change Bug Status");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            var bugs = _developerRepo.ViewAssignedBugs(developer.UserId);
            if (!bugs.Any())
            {
                Console.WriteLine("You have no assigned bugs.");
                return;
            }

            Console.WriteLine("Select a bug to update status:");
            foreach (var bug in bugs)
            {
                Console.WriteLine($"{bug.BugId}: {bug.BugName} - {bug.Status}");
            }

            Console.Write("Enter Bug ID: ");
            if (!int.TryParse(Console.ReadLine(), out var bugId) || bugs.All(b => b.BugId != bugId))
            {
                Console.WriteLine("Invalid Bug ID.");
                return;
            }

            Console.Write("Enter new status (Open/Closed): ");
            var newStatus = Console.ReadLine()?.Trim();
            if (newStatus != "Open" && newStatus != "Closed")
            {
                Console.WriteLine("Invalid status. Please enter 'Open' or 'Closed'.");
                return;
            }

            _developerRepo.ChangeBugStatus(bugId, newStatus);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Bug status updated successfully.");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
