using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class MakeCourseList
    {
        public static List<Course> makeCourseList(){
            List<Course> allCourses = new List<Course>();
                //string subject, string teacher, string[] groups, string room, int repetitions
            allCourses.Add(makeNewCourse("French", "Adam", ["A"],"224",4));
            allCourses.Add(makeNewCourse("French", "Adam", ["B"],"224",4));
            allCourses.Add(makeNewCourse("Math", "Mike", ["A"],"221",4));
            allCourses.Add(makeNewCourse("Math", "Mike", ["B"],"221",4));
            allCourses.Add(makeNewCourse("Music", "Mike", ["B"],"113",2));
            allCourses.Add(makeNewCourse("Music", "Mike", ["F","G"],"113",2));
            allCourses.Add(makeNewCourse("Music", "Mike", ["D"],"113",2));
            allCourses.Add(makeNewCourse("Music", "Mike", ["NV"],"NV",2));
            allCourses.Add(makeNewCourse("Music", "Mike", ["JS"],"JS",2));
            return allCourses;
        }

        public static Course makeNewCourse(string subj, string tchr, string[] grps, string rm, int reps){
            //Initialize new ourse
            Course newCourse = new Course();
            //Assign the variables
            newCourse.subject = subj;
            newCourse.teacher = tchr;
            foreach(string g in grps){
                newCourse.groups.Add(g);
            }
            newCourse.room = rm;
            newCourse.repetitions = reps;
            //Format name as: Subject (space) Groups
            newCourse.courseName = newCourse.subject + " ";
            newCourse.groups.ForEach(x => {
                newCourse.courseName += x;
            });
            
            return newCourse;
        }
    }
}