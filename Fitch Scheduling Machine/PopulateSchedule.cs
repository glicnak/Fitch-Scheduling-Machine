using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class PopulateSchedule{

    public static void populateSchedule(Course[,,] schedule2dArray, List<Course> allCourses, List<string> allGroups, int daysPerCycle, int periodsPerDay, int numGroups){
        
        Dictionary<Course, int> courseCount = new Dictionary<Course, int>();
        foreach (Course c in allCourses){
            courseCount[c] = c.repetitions;
        }

        List<Course> availableCourses = new List<Course>(allCourses);
        List<Course> coursesUsedInDay = new List<Course>();

        Dictionary<Course,List<int[]>> forcedCourses = ForcedCourses.placeCourses(allCourses);

        foreach(Course key in forcedCourses.Keys){
            courseCount[key]+= -forcedCourses[key].Count;
        }

        populateArray(schedule2dArray, allCourses, forcedCourses, allGroups, courseCount, availableCourses, coursesUsedInDay, daysPerCycle, periodsPerDay, numGroups, 0, 0, 0);
    }

    static bool populateArray(Course[,,] schedule2dArray, List<Course> allCourses, Dictionary<Course,List<int[]>> forcedCourses, List<string> allGroups, Dictionary<Course, int> courseCount, List<Course> availableCourses, List<Course> coursesUsedInDay, int daysPerCycle, int periodsPerDay, int numGroups, int x, int y, int z){

        List<Course> newAvailableCoursesBackup = new List<Course>();
        availableCourses.ForEach(c=>{
            newAvailableCoursesBackup.Add(c);
        });
        List<Course> newCoursesUsedInDay = new List<Course>();
        coursesUsedInDay.ForEach(c=>{
            newCoursesUsedInDay.Add(c);
        });

        // Calculate next index
        int nextX = x;
        int nextY = y;
        int nextZ = z;

        //Checkgroups used and not used in Period
        List<string> groupsUsedInPeriod = new List<string>();
        for(int i=0;i<z;i++){
            if(schedule2dArray[x,y,i].group != null){
                groupsUsedInPeriod.Add(schedule2dArray[x,y,i].group);
            }
        }

        if (availableCourses.Count == 0){ //If there are no available courses, we're at the end of 1 period...
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
                newCoursesUsedInDay.Clear();//Reset courses used in a day at the start of the day
            }
            newAvailableCoursesBackup.Clear(); // Reset available courses based on usedInDay
            foreach(Course c in courseCount.Keys){
                if(courseCount[c] > 0 && !newCoursesUsedInDay.Contains(c)){
                    newAvailableCoursesBackup.Add(c);
                }
            }
        }

        //Force Add any classes
        List<Course> forcedCoursesThisPeriod = forcedCourses
            .Where(entry => entry.Value.Any(arr => arr.SequenceEqual([nextX,nextY])))
            .Select(entry => entry.Key)
            .ToList();

        if(forcedCoursesThisPeriod.Count>0){
            addCoursesToSchedule(schedule2dArray, allCourses, forcedCoursesThisPeriod, nextX,nextY,nextZ, courseCount, newAvailableCoursesBackup, newCoursesUsedInDay);
            nextZ += forcedCoursesThisPeriod.Count;
            forcedCoursesThisPeriod.ForEach(c=>{
                forcedCourses[c].RemoveAll(arr => arr.SequenceEqual([nextX,nextY]));
            });
        }

        //Check if there are available courses for every group thats left
        else{
            List<string> groupsLeft =  allGroups.Except(groupsUsedInPeriod).ToList();
            if(!groupsLeft.All(g => availableCourses.Any(c => c.group == g))){
                return false;
            }
        }

        //Check at the end of day: any class has more repetitions than days, any teacher has more periods than periods left in total

        //if (nextX == 2 && nextY == 4 && nextZ == 8) // If we're at the end of day X
        if (nextX == daysPerCycle) // If we're at the end of 1 cycle
        //if (nextX == daysPerCycle && courseCount.Values.All(count => count == 0)) // If we're at the end of 1 cycle, its the last day, check if courseCount[every course] == 0
        {
            printArray(schedule2dArray, courseCount, daysPerCycle, periodsPerDay, numGroups);
            //Debug
            Console.WriteLine();
            int counter =0;
            newAvailableCoursesBackup.ForEach(c=>{
                Console.Write(c.courseName + ", ");
                counter+= courseCount[c];
            });
            Console.WriteLine();
            Console.WriteLine("Classes left: " + counter + ". Courses left: " + newAvailableCoursesBackup.Count);
            Console.WriteLine();
            Console.WriteLine("Courses used in day: " + newCoursesUsedInDay.Count);
            Console.WriteLine();
            Console.WriteLine("X: " + nextX + "Y: " + nextY + "Z: " + nextZ);
            return true;
        }

        // Try placing each string in the current cell
        foreach (Course c in newAvailableCoursesBackup)
        {
            //Backup
            List<Course> newAvailableCourses = new List<Course>();
            newAvailableCoursesBackup.ForEach(c=>{
                newAvailableCourses.Add(c);
            });

            //Make a list of all courses with the same link (or just the class itself if it doesn't have one)
            List<Course> linkedCourses = new List<Course>();
            if (c.link != null && c.link !=""){
                newAvailableCourses.ForEach(a=>{
                    if (c.link == a.link){
                        linkedCourses.Add(a);
                    }
                });
            }
            else {
                linkedCourses.Add(c);
            }

            //Check that there are at least as many days as courseCount left
            //if(linkedCourses.All(l =>courseCount[l]-1 < daysPerCycle-nextX)){
                //Add courses
                addCoursesToSchedule(schedule2dArray, allCourses, linkedCourses, nextX,nextY,nextZ, courseCount, newAvailableCourses, newCoursesUsedInDay);
                nextZ += linkedCourses.Count-1;
                // Recurse to the next cell
                if (populateArray(schedule2dArray, allCourses, forcedCourses, allGroups, courseCount, newAvailableCourses, newCoursesUsedInDay, daysPerCycle, periodsPerDay, numGroups, nextX, nextY, nextZ+1))
                {
                    return true; // Comment this if you want to see all configurations, not just the first one
                }

                // Backtrack
                nextZ += -(linkedCourses.Count-1);
                removeCoursesFromSchedule(schedule2dArray,linkedCourses, nextX, nextY, nextZ, courseCount, availableCourses, newCoursesUsedInDay);
                Console.WriteLine("Backtrack!");
            //}
        }
        
        return false;
    }

        public static void addCoursesToSchedule (Course[,,]schedule2dArray, List<Course> allCourses, List<Course> linkedCourses, int x, int y, int z, Dictionary<Course, int> courseCount, List<Course> availableCourses, List<Course> coursesUsedInDay){
            //Add courses to schedule, decrement course count, and add it to courses used in day and remove it from available courses
            for (int i=0; i<linkedCourses.Count; i++){
                schedule2dArray[x, y, z+i] = linkedCourses[i];
                Console.WriteLine("Added class " + linkedCourses[i].courseName + " to day " + (x+1) + " period " + (y+1) + " rank " +(z+i));
                courseCount[linkedCourses[i]]--;
                coursesUsedInDay.Add(linkedCourses[i]);
                availableCourses.Remove(linkedCourses[i]);

                //Remove all courses with the same group or teacher from available courses
                List<Course> associatedClasses = new List<Course>();
                allCourses.ForEach(c=>{
                    //if(c.teacher == linkedCourses[i].teacher || c.group == linkedCourses[i].group){
                    if(c.teacher == linkedCourses[i].teacher || c.group == linkedCourses[i].group || c.room == linkedCourses[i].room){
                        associatedClasses.Add(c);
                    }
                });
                associatedClasses.ForEach(c=>{
                    availableCourses.RemoveAll(course => course.link != null && course.link != "" && course.link == c.link);
                    availableCourses.Remove(c);
                });
            }

        }

        public static void removeCoursesFromSchedule (Course[,,]schedule2dArray, List<Course> linkedCourses, int x, int y, int z, Dictionary<Course, int> courseCount, List<Course> availableCourses, List<Course> coursesUsedInDay){
            //Remove courses from schedule, increment course count back up, and remove it from courses used in day and add it to available courses
            for (int i=0; i<linkedCourses.Count; i++){
                schedule2dArray[x, y, z+i] = null;
                courseCount[linkedCourses[i]]++;
                coursesUsedInDay.Remove(linkedCourses[i]);
            }
        }

        public static void printArray(Course[,,] schedule2dArray, Dictionary<Course,int> courseCount, int daysPerCycle, int periodsPerDay, int numGroups){
            int totalClassesPlaced = 0;
            int totalClasses=0;
            // Print the 3D array with spaces between the end of each sub-array
            for (int i = 0; i < daysPerCycle; i++)
            {
                Console.WriteLine($"Day {i+1}:");
                for (int j = 0; j < periodsPerDay; j++)
                {
                    //Order
                    List<Course> orderedGroups = new List<Course>();
                    for (int k =0; k < numGroups; k++){
                        if (schedule2dArray[i,j,k] != null){
                            orderedGroups.Add(schedule2dArray[i,j,k]);
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
                            totalClassesPlaced++;
                        }
                    }
                    Console.WriteLine(); // Newline after each sub-array
                }
                Console.WriteLine(); // Extra newline for spacing between layers
            }
            List<Course> allCourses = new List<Course>(courseCount.Keys);
            allCourses.ForEach(x=>{
                totalClasses += x.repetitions;
            });
            Console.WriteLine("We managed to place " + totalClassesPlaced + " out of " + totalClasses + " classes!");
        }


    }
}