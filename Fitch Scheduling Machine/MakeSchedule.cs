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
            Course[,,] schedule2dArray= new Course[daysPerCycle,periodsPerDay,numGroups];

            //Populate the schedule
            populateSchedule(schedule2dArray, allCourses, daysPerCycle, periodsPerDay, numGroups);

            // Print the 3D array with spaces between the end of each sub-array
            for (int i = 0; i < daysPerCycle; i++)
            {
                Console.WriteLine($"Day {i+1}:");
                for (int j = 0; j < periodsPerDay; j++)
                {
                    for (int k = 0; k < numGroups; k++)
                    {
                        string endingString ="";
                        if(k<numGroups-1){
                            endingString += ", ";
                        }
                        Console.Write(schedule2dArray[i,j,k].courseName + endingString);
                    }
                    Console.WriteLine(); // Newline after each sub-array
                }
                Console.WriteLine(); // Extra newline for spacing between layers
            }



        }

        public static void populateSchedule(Course[,,] schedule2dArray, List<Course> allCourses, int daysPerCycle, int periodsPerDay, int numGroups){
            
        //Dictionary to ensure no course is present more than Rep times
            Dictionary<Course, int> courseCount = allCourses.ToDictionary(c => c, c =>0);

            Random random = new Random();
            
        //Per Cycle
            for (int i = 0; i < daysPerCycle; i++){
        //Per Day
                for (int j = 0; j < periodsPerDay; j++){
                    //Make sure no class is repeated twice in the day
                    List<Course> usedInDay = new List<Course>(); // Track Courses used in this [i,j]
        //Per Period
                    for (int k = 0; k < numGroups; k++){
                        Course selectedCourse = null;
                        List<Course> availableCourses = allCourses
                            .Where(c => courseCount[c] < c.repetitions && !usedInDay.Contains(c))
                            .ToList();

                        if (availableCourses.Count == 0)
                        {
                            //throw new InvalidOperationException("Not enough strings to fill the array with the given constraints.");
                            Course empty = new Course(){};
                            empty.courseName = "Empty";
                            availableCourses.Add(empty);
                        }

                        selectedCourse = availableCourses[random.Next(availableCourses.Count)];

                        // Assign string to the array
                        schedule2dArray[i, j, k] = selectedCourse;

                        // Track its usage
                        if(selectedCourse.courseName != "Empty"){
                            courseCount[selectedCourse]++;
                            usedInDay.Add(selectedCourse);
                        }
                    }
                }
            }
        }
    }
}