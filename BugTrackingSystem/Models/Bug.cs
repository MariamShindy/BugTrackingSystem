using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.Models
{
    public class Bug
    {
        public int BugId { get; set; }
        public string BugName { get; set; } = string.Empty;
        public string BugType { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public DateTime BugDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
        public int DeveloperId { get; set; }
        public double HoursSpent { get; set; }
    }
}
