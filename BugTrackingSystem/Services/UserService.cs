using BugTrackingSystem.Models;
using BugTrackingSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.Services
{
    public class UserService(AdminRepository adminRepo ,ProjectManagerRepository projectManagerRepo)
    {
        public User AuthenticateUser(string username, string password)
        {
            return adminRepo.GetUserByUsernameAndPassword(username, password);
        }
        public bool IsValidUser(int id,string? role)
        {
            var user = adminRepo.GetUserById(id);
            if (role is null)
                return user is not null;
            else
                return user is not null && role.ToLower() == user.Role;
        }
        public bool IsValidProject(int id)
        {
            var project = projectManagerRepo.MonitorBugs(id,true);
            return project is not null;
        }
    }
}
