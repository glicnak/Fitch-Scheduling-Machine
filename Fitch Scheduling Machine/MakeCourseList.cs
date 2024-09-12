using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class MakeCourseList
    {
        public static List<Course> makeCourseList(){
            //Initialize allCoursesArrayFromSource as an array of arrays (Courses) of arrays (properties of each course). This should be generated from the user input.
            //string subject, string teacher, string[] groups, string room, int repetitions
            string[][][] allCoursesArrayFromSource = new string[][][]{
                [["French"],[ "Adam"], ["A"],["224"],["4"]],
                [["French"],[ "Adam"], ["A"],["224"],["4"]],
                [["French"],[ "Adam"], ["A"],["224"],["2"]],
                [["French"],[ "Adam"], ["B"],["224"],["4"]],
                [["Phys Ed"],[ "Adam"], ["B"],["Gym"],["2"]],
                [["Phys Ed"],[ "Adam"], ["D"],["Gym"],["2"]],
                [["Phys Ed"],[ "Adam"], ["FG"],["Gym"],["2"]],
                [["Out Ed"],[ "Adam"], ["FG"],["Gym"],["4"]],
                [["Math"],[ "Mike"], ["A"],["224"],["4"]],
                [["Math"],[ "Mike"], ["B"],["224"],["4"]],
                [["Music"],[ "Mike"], ["D"],["113"],["2"]],
                [["Music"],[ "Mike"], ["F","G"],["113"],["2"]],
                [["Music"],[ "Mike"], ["NV"],["NV"],["2"]],
                [["Music"],[ "Mike"], ["JS"],["JS"],["2"]],
                };
            
            //Make the list of every course.
            List<Course> allCourses = new List<Course>();
            foreach(string[][] a in allCoursesArrayFromSource){

                //Make the new course
                Course newCourse = makeNewCourse(a);

                //Don't add if a course has the same name
                bool uniqueCourse = true;
                allCourses.ForEach(x=>{
                    if(x.courseName == newCourse.courseName){
                        uniqueCourse = false;
                    }
                });
                if(uniqueCourse){
                    allCourses.Add(newCourse);
                }
            }   
            return allCourses;
        }

        //Function to take string[][] and turn it into a Course
        public static Course makeNewCourse(string[][] a){

            //Initialize new ourse
            Course newCourse = new Course();

            //Assign the variables
            newCourse.subject = a[0][0].ToString();
            newCourse.teacher = a[1][0].ToString();
            foreach(string g in a[2]){
                newCourse.groups.Add(g);
            }
            newCourse.room = a[3][0].ToString();

            //Number of Reps
            int.TryParse(a[4][0], out int val);
            newCourse.repetitions = val;

            //Format name as: Subject (space) Groups
            newCourse.courseName = newCourse.subject + " ";
            newCourse.groups.ForEach(x => {
                newCourse.courseName += x;
            });
            
            return newCourse;
        }
        
    }
}