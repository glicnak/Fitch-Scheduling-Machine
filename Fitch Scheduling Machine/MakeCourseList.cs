using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class MakeCourseList
    {
        public static List<Course> makeCourseList(){
        //Initialize allCoursesArrayFromSource as a list of arrays (Courses) of strings (properties of each course). This should be generated from the user input, right now coming from ClassList.txt.
        //subject, repetitions, groups, teacher, room
            List<string[]> allCoursesArrayFromInput = new List<string[]>();

            string[] fileLines = readFile("courseInfo");

            //Make courses array from the rest
            for (int i=1; i<fileLines.Length;i++){
                // Split each line by commas to get individual elements
                string[] stringElements = fileLines[i].Split(',');
                allCoursesArrayFromInput.Add(stringElements);
            }

            
        //Make the list of every course.
            List<Course> allCourses = new List<Course>();
            allCoursesArrayFromInput.ForEach( a=> {

                //Make the new course
                Course newCourse = makeNewCourse(a);

                if(newCourse!=null){
                    allCourses.Add(newCourse);
                }
            }); 

            return allCourses;
        }


    //Function to take string[][] and turn it into a Course
        public static Course? makeNewCourse(string[] a){
            //If array length is less than 5, it is poorly formatted. Return null
            if(a.Length<5){
                return null;
            }

            //Initialize new ourse
            Course newCourse = new Course();

            //Assign the Subject, Teacher & Room
            newCourse.subject = trimCapitalizeFirstLetter(a[0]);
            newCourse.group = (trimCapitalizeFirstLetter(a[2]));
            newCourse.teacher = trimCapitalizeFirstLetter(a[3]);
            newCourse.room = trimCapitalizeFirstLetter(a[4]);

            //Assign the number of Reps. If 0, it means reps were a string. Return null.
            int.TryParse(a[1], out int val);
            if (val == 0){
                return null;
            }
            newCourse.repetitions = val;

            //Assign link
            if(a.Length>5 && a[5] != null && a[5]!=""){
                newCourse.link = trimCapitalizeFirstLetter(a[5]);
            }

            //Format name as: Subject (space) Groups
            newCourse.courseName = newCourse.subject + " " + newCourse.group;
            
            return newCourse;
        }

        public static string trimCapitalizeFirstLetter(string strRaw)
        {
            //Remove spaces at the front and at the end of the string
            string str = strRaw.Trim(); 
            
            //Check if empty
            if (string.IsNullOrEmpty(str))
                return str;

            // Capitalize the first letter and concatenate with the rest of the string
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string[] readFile(string infoRequest){
            // Path to the text file
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..","..","ClassList.txt");

            // Read all lines from the file
            string[] fileLines = File.ReadAllLines(filePath);

            //Make an info array 'scheduleInfo' from the first line
            //Days per cycle, Periods per day, Number of groups
            string[] scheduleInfo = fileLines[0].Split(',');
            foreach(string a in scheduleInfo){
                trimCapitalizeFirstLetter(a);
            }
            if (infoRequest == "scheduleInfo"){
                return scheduleInfo;
            }        
            else {
                return fileLines;
            }
        }
    }
}