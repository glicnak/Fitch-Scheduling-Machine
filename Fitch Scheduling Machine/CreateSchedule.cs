using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class CreateSchedule
    {
        public static Course[,,] makeSchedule(List<Course> allCourses){

            //Grab the day, period, and group info from MakeCourseLists
            string[] scheduleInfo = MakeCourseList.readFile("scheduleInfo");
            
            //Days Per Cycle, Periods per Day, Number of Groups
            int.TryParse(scheduleInfo[0], out int daysPerCycle); 
            int.TryParse(scheduleInfo[1], out int periodsPerDay);  

            //Get a list of all groups
            List<string> allGroups = new List<string>();
            for (int i=2;i<scheduleInfo.Length;i++){
                allGroups.Add(scheduleInfo[i]);
            }

            //Number of possible slots in the lowest dimension of the array
            int numGroups = getNumGroups(allCourses); 

            //Make the Jagged array of x groups in y periods in z days
            Course[,,] schedule3dArray= new Course[daysPerCycle,periodsPerDay,numGroups];

            //Keep track of the reps left for every Course
            Dictionary<Course, int> courseCount = new Dictionary<Course, int>();
            foreach (Course c in allCourses){
                courseCount[c] = c.repetitions;
            }

            //Add the forced Classes
            addForcedCourses(schedule3dArray,ForcedCourses.placeCourses(allCourses),courseCount);

            //List of all courses available
            List<Course> availableCourses = courseCount
                .Where(entry => entry.Value > 0)
                .Select(entry => entry.Key)
                .ToList();
            //List of courses used in a day
            List<Course> coursesUsedInDay = new List<Course>();

            //Populate the schedule
            populateSchedule(schedule3dArray, allCourses, allGroups, courseCount, availableCourses, coursesUsedInDay, daysPerCycle, periodsPerDay, numGroups,0,0,0);

            //Debug print extra or underused courses
            foreach(var entry in courseCount){
                if(entry.Value != 0){
                    Console.WriteLine("Over or under-used Course: " + entry.Key.courseName + " " + entry.Value);
                }
            }

            //Debug Print the schedule
            int totalCourses = 0;
            int totalCoursesPlaced = 0;
            allCourses.ForEach(x=>{
                totalCourses += x.repetitions;
            });
            printArray(schedule3dArray, allCourses, daysPerCycle, periodsPerDay, numGroups, totalCoursesPlaced, totalCourses);

            //Return the 3d array
            return schedule3dArray;
        }

        public static int getNumGroups(List<Course> allCourses){
            List<string> diffGroupsAndLinks = new List<string>();
            double numGroups = 0;
            allCourses.ForEach(x=>{
                if(x.link !=null && x.link != ""){
                    if (!diffGroupsAndLinks.Contains(x.link)){
                    diffGroupsAndLinks.Add(x.link);
                    numGroups+= 0.5;
                    }
                }
                else if (!diffGroupsAndLinks.Contains(x.group)){
                    diffGroupsAndLinks.Add(x.group);
                    numGroups++;
                }

            });
            return (int)Math.Ceiling(numGroups);;
        }

        public static List<Course> shuffleList(List<Course> list){
            Random rng = new Random();
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Course value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static void addForcedCourses(Course[,,] schedule3dArray, Dictionary<Course,List<int[]>> forcedCourses, Dictionary<Course,int> courseCount){
            foreach (var entry in forcedCourses){
                foreach (int[] dayPeriod in entry.Value){
                    int day = dayPeriod[0];
                    int period = dayPeriod[1];

                    // Loop through the z dimension to find the lowest empty z slot at (day, period)
                    for (int z = 0; z < schedule3dArray.GetLength(2); z++){
                        // Check if the slot is empty (null or default value)
                        if (schedule3dArray[day, period, z] == null){
                            // Place the course at the lowest available z value
                            schedule3dArray[day, period, z] = entry.Key;
                            courseCount[entry.Key]--;
                            break; // Exit the z loop once the course is placed
                        }
                    }
                }
            }
        }

        public static bool populateSchedule(Course[,,] schedule3dArray, List<Course> allCourses, List<string> allGroups, Dictionary<Course, int> courseCount, List<Course> availableCourses, List<Course> coursesUsedInDay, int daysPerCycle, int periodsPerDay, int numGroups, int x, int y, int z){
            List<Course> availableCoursesBackup = new List<Course>(availableCourses);
            List<Course> coursesUsedInDayBackup = new List<Course>(coursesUsedInDay);

            // Calculate next index
            int nextX = x;
            int nextY = y;
            int nextZ = z+1;

            //update available Courses by looking forward into the period

            //Backup available courses and courses used in Day... then update courses used in day in the nextperiod/nextday checks... maybe I can just back them up after those checks?

            //Checkgroups used and not used in Period
            List<string> groupsUsedInPeriod = new List<string>();
            for(int i=0;i<nextZ;i++){
                if(schedule3dArray[x,y,i].group != null){
                    groupsUsedInPeriod.Add(schedule3dArray[x,y,i].group);
                }
            }

            //Check if there are available courses for every group thats left, otherwise return false
            List<string> groupsLeft =  allGroups.Except(groupsUsedInPeriod).ToList();
            if(!groupsLeft.All(g => availableCoursesBackup.Any(c => c.group == g))){
                return false;
            }

            //Check for a change in period or day
            if (availableCoursesBackup.Count == 0){ //If there are no available courses, we're at the end of 1 period...
                //Check if every group is present at least once before moving to the next day
                if(!allGroups.All(group => groupsUsedInPeriod.Contains(group))){
                    return false;
                }
                //Move to the next day
                nextZ = 0;
                nextY = y+1;
                if (nextY == periodsPerDay){ //If we're at the end of 1 day
                    //Go to next day
                    nextX = x+1;
                    nextY = 0;
                    coursesUsedInDayBackup.Clear();//Reset courses used in a day at the start of the day
                }
                availableCoursesBackup.Clear(); // Reset available courses based on usedInDay
                foreach(Course c in courseCount.Keys){
                    if(courseCount[c] > 0 && !coursesUsedInDayBackup.Contains(c)){
                    // if we want to be able to double up courses, just use  --  if(courseCount[c] > 0){
                        availableCoursesBackup.Add(c);
                    }
                }
            }

            //Check for end of cycle
            if (nextX == daysPerCycle) // If we're at the end of 1 cycle
            //if (nextX == daysPerCycle && courseCount.Values.All(count => count == 0)) // If we're at the end of 1 cycle, its the last day, check if courseCount[every course] == 0
            {
                return true;
            }

            //Return false if we reach the end
            return false;
        }

        public static void printArray(Course[,,] schedule3dArray, List<Course> allCourses, int daysPerCycle, int periodsPerDay, int numGroups, int totalCoursesPlaced, int totalCourses){
            // Print the 3D array with spaces between the end of each sub-array
            for (int i = 0; i < daysPerCycle; i++)
            {
                Console.WriteLine($"Day {i+1}:");
                for (int j = 0; j < periodsPerDay; j++)
                {
                    //Order
                    List<Course> orderedGroups = new List<Course>();
                    for (int k =0; k < numGroups; k++){
                        if (schedule3dArray[i,j,k] != null){
                            orderedGroups.Add(schedule3dArray[i,j,k]);
                        }
                    }
                    // Sort courses by repetitions in descending order
                    orderedGroups.Sort((c2, c1) => c2.group.CompareTo(c1.group));
                    for (int k = 0; k < orderedGroups.Count; k++)
                    {
                        string endingString ="";
                        if(k<orderedGroups.Count-1){
                            endingString += ", ";
                        }
                        if(orderedGroups[k] != null){
                            Console.Write(orderedGroups[k].courseName + endingString);
                            totalCoursesPlaced++;
                        }
                    }
                    Console.WriteLine(); // Newline after each sub-array
                }
                Console.WriteLine(); // Extra newline for spacing between layers
            }
            
            Console.WriteLine("We managed to place " + totalCoursesPlaced + " out of " + totalCourses + " classes!");
        }

    }
}