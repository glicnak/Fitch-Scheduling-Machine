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
            //subject, repetitions, teacher, room, groups
            string[][] allCoursesArrayFromSource = new string[][]{
                ["French", "4", "Adam", "224", "A"],
                ["French", "4", "Adam", "224", "B"],
                ["Phys Ed", "2", "Adam", "Gym", "B"],
                ["Phys Ed", "2", "Adam", "Gym", "D"],
                ["Phys Ed", "2", "Adam", "Gym", "F", "G"],
                ["Out Ed", "2", "Adam", "Gym", "F", "G"],
                ["Music", "2", "Mike", "113", "D"],
                ["Music", "2", "Mike", "113", "F", "G"],
                ["Music", "2", "Mike", "NV", "NV"],
                ["Music", "2", "Mike", "JS", "JS"],
                };
            
            //Make the list of every course.
            List<Course> allCourses = new List<Course>();
            foreach(string[] a in allCoursesArrayFromSource){

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
        public static Course makeNewCourse(string[] a){

            //Initialize new ourse
            Course newCourse = new Course();

            //Assign the Subject, Teacher & Room
            newCourse.subject = a[0].ToString();
            newCourse.teacher = a[2].ToString();
            newCourse.room = a[3].ToString();

            //Assign the number of Reps
            int.TryParse(a[1], out int val);
            newCourse.repetitions = val;

            //Assign the groups and sort them alphabetically
            for (int i=4; i<a.Length;i++){
                newCourse.groups.Add(a[i]);
            }
            newCourse.groups.Sort();

            //Format name as: Subject (space) Groups
            newCourse.courseName = newCourse.subject + " ";
            newCourse.groups.ForEach(x => {
                newCourse.courseName += x;
            });
            
            return newCourse;
        }
        
    }
}