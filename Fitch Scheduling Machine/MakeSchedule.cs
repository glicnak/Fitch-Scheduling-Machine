using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class MakeSchedule
    {
        public static void makeSchedule(List<Course> allCourses){

            //Grab the info from MakeCourseLists
            string[] scheduleInfo = MakeCourseList.readFile("scheduleInfo");
            
            //Days Per Cycle, Periods per Day, Number of Groups
            int.TryParse(scheduleInfo[0], out int daysPerCycle); 
            int.TryParse(scheduleInfo[1], out int periodsPerDay);  
            int.TryParse(scheduleInfo[2], out int numGroups);  

            //Make the Jagged array of x groups in y periods in z days
            Course[][][] schedule2dArray= new Course[daysPerCycle][][];
            for (int i=0;i<daysPerCycle;i++){
                schedule2dArray[i] = new Course[periodsPerDay][];
                for (int j=0; j<periodsPerDay; j++){
                    schedule2dArray[i][j] = new Course[numGroups];
                }
            }

            //Populate the schedule
            populateSchedule(schedule2dArray, allCourses);

            // Print the 3D array with spaces between the end of each sub-array
            for (int i = 0; i < schedule2dArray.Length; i++)
            {
                Console.WriteLine($"Day {i+1}:");
                for (int j = 0; j < schedule2dArray[i].Length; j++)
                {
                    for (int k = 0; k < schedule2dArray[i][j].Length; k++)
                    {
                        Console.Write(schedule2dArray[i][j][k].courseName + ", ");
                    }
                    Console.WriteLine(); // Newline after each sub-array
                    Console.WriteLine(); // Extra newline for spacing between sub-arrays
                }
                Console.WriteLine(); // Extra newline for spacing between layers
            }

        }

        public static void populateSchedule(Course[][][] schedule2dArray, List<Course> allCourses){
            for (int i = 0; i < schedule2dArray.Length; i++){
                for (int j = 0; j < schedule2dArray[i].Length; j++){
                    for (int k = 0; k < schedule2dArray[i][j].Length; k++){
                        //Populate randomly
                        Random random = new Random();
                        int randomIndex = random.Next(allCourses.Count);
                        Course randomCourse = allCourses[randomIndex];
                        schedule2dArray[i][j][k] = randomCourse;
                    }
                }
            }
        }
    }
}