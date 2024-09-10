using BugTrackingSystem.Repositories;
using BugTrackingSystem.Services;
using BugTrackingSystem.Utilities;

namespace BugTrackingSystem.Menus
{
    public class AdminMenu(AdminRepository _adminRepo , UserService _userService)
    {
        public void ShowMenu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("*************************************");
                Console.WriteLine("Admin Menu");
                Console.WriteLine("*************************************");
                Console.ResetColor();
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Update User");
                Console.WriteLine("3. Delete User");
                Console.WriteLine("4. View All Users");
                Console.WriteLine("5. Add Project");
                Console.WriteLine("6. View All Projects");
                Console.WriteLine("7. View All Bugs");
                Console.WriteLine("8. Logout");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddUser();
                        break;
                    case "2":
                        UpdateUser();
                        break;
                    case "3":
                        DeleteUser();
                        break;
                    case "4":
                        ViewAllUsers();
                        break;
                    case "5":
                        AddProject();
                        break;
                    case "6":
                        ViewAllProjects();
                        break;
                    case "7":
                        ViewAllBugs();
                        break;
                    case "8":
                        return;
                    case "9":
                        Environment.Exit(0); 
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        private void AddUser()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("Add User");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Utility.ReadPassword();
            Console.WriteLine();
            Console.Write("Role (e.g., Tester, Developer, PM, Admin): ");
            var role = Console.ReadLine();

            _adminRepo.AddUser(username, password, role);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("User added successfully.");
            Console.ResetColor();
            Console.WriteLine();

        }

        private void UpdateUser()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("Update User");   
            Console.WriteLine("*************************************");
            Console.ResetColor();
            ViewAllUsers();
            int userId;
            bool isValidInput;

            do
            {
                Console.Write("Enter User ID: ");
                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out userId);

                if (!isValidInput || !_userService.IsValidUser(userId,null))
                {
                    Console.WriteLine("Invalid User ID. Please enter a valid User ID.");
                    isValidInput = false; 
                }
            } while (!isValidInput);
            Console.Write("New Username: ");
            var username = Console.ReadLine();
            Console.Write("New Password: ");
            var password = Utility.ReadPassword();
            Console.WriteLine();
            Console.Write("New Role: ");
            var role = Console.ReadLine();

            _adminRepo.UpdateUser(userId, username, password, role);
            Console.WriteLine("User updated successfully.");
        }

        private void DeleteUser()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("Delete User");
            Console.WriteLine("*************************************");
            ViewAllUsers();
            Console.ResetColor();
            int userId;
            bool isValidInput;

            do
            {
                Console.Write("Enter User ID: ");
                var input = Console.ReadLine();
                isValidInput = int.TryParse(input, out userId);

                if (!isValidInput || !_userService.IsValidUser(userId, null))
                {
                    Console.WriteLine("Invalid User ID. Please enter a valid User ID.");
                    isValidInput = false;
                }
            } while (!isValidInput);

            _adminRepo.DeleteUser(userId);
            Console.WriteLine("User deleted successfully.");
        }

        private void ViewAllUsers()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("All Users");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            var users = _adminRepo.ViewAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"{user.UserId} --> : username --> {user.Username}, role--> {user.Role}");
            }
        }
        private void AddProject()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("Add Project");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            string projectName;
            Console.WriteLine("Please enter the project name");
            projectName = Console.ReadLine();
            do
            {
                projectName = Console.ReadLine();
                Console.WriteLine("Please enter the project name");
            } while (string.IsNullOrEmpty(projectName));
            _adminRepo.AddProject(projectName);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Project added successfully.");
            Console.ResetColor();
            Console.WriteLine();
        }
        private void ViewAllProjects()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("All Projects");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            var projects = _adminRepo.ViewAllProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($"{project.ProjectId} --> {project.ProjectName}");
            }
        }
        private void ViewAllBugs()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*************************************");
            Console.WriteLine("View All Bugs");
            Console.WriteLine("*************************************");
            Console.ResetColor();
            var bugs = _adminRepo.ViewAllBugs();
            foreach (var bug in bugs)
            {
                Console.WriteLine($"{bug.BugId}: {bug.BugName} - {bug.Status}");
            }
        }
        
    }
}
