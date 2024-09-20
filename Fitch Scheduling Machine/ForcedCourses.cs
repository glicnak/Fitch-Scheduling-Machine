using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class ForcedCourses
    {
        public static Dictionary<Course,string> placeCourses(List<Course> allCourses){

            Dictionary<Course,string> forcedCourses= new Dictionary<Course,string>(){
                {allCourses.Find(c => c.courseName == "Out Ed A"),"0,0,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed B"),"0,0,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed C"),"0,1,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed D"),"0,1,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed E"),"0,2,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed F"),"0,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed G"),"0,2,2,"},
                
                {allCourses.Find(c => c.courseName == "Out Ed A"),"4,4,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed B"),"4,4,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed C"),"4,5,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed D"),"4,5,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed E"),"4,6,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed F"),"4,6,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed G"),"4,6,2,"},
                
                {allCourses.Find(c => c.courseName == "Out Ed Ex A"),"1,1,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex A"),"1,2,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex B"),"1,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex B"),"1,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex C"),"2,2,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex C"),"2,2,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex D"),"2,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex D"),"2,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex E"),"3,2,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex E"),"3,2,0,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex F"),"3,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex F"),"3,2,1,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex G"),"3,2,2,"},
                {allCourses.Find(c => c.courseName == "Out Ed Ex G"),"3,2,2,"}            
            };
            return forcedCourses;
        }
    }
}