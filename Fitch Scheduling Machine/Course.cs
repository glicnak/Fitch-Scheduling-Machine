using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class Course
    {
        public string courseName = "";
        public string subject = "";
        public string teacher = "";
        public List<string> groups = new List<string>();
        public string room = "";
        public int repetitions = 0;
    }
}