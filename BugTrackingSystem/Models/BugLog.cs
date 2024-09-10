using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackingSystem.Models
{
    public class BugLog
    {
        public int BugLogId { get; set; }
        public int BugId { get; set; }
        public int DeveloperId { get; set; }
        public int TimeSpent { get; set; } // Time spent in minutes
    }
}
