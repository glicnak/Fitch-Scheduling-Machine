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

            int numGroups = getNumGroups(allCourses); 

            List<string> allGroups = new List<string>();
            for (int i=2;i<scheduleInfo.Length;i++){
                allGroups.Add(scheduleInfo[i]);
            }

            //Make the Jagged array of x groups in y periods in z days
            Course[,,] schedule2dArray= new Course[daysPerCycle,periodsPerDay,numGroups];

            //Populate the schedule
            //populateSchedule(schedule2dArray, allCourses, allGroups, daysPerCycle, periodsPerDay, numGroups);
            //PopulateSchedule.printArray(schedule2dArray, allCourses, daysPerCycle, periodsPerDay, numGroups);

            //Recursively Populate the schedule
            List<Course> orderedCourses = allCourses.OrderBy(course => course.repetitions).ToList();
            PopulateSchedule.populateSchedule(schedule2dArray, orderedCourses, allGroups, daysPerCycle, periodsPerDay, numGroups);

        }

        public static void populateSchedule(Course[,,] schedule2dArray, List<Course> allCourses, List<string> allGroups, int daysPerCycle, int periodsPerDay, int numGroups){
            
        //Dictionary to ensure no course is present more than Rep times
            Dictionary<Course, int> courseCount = allCourses.ToDictionary(c => c, c =>0);
            
        //Per Cycle
            for (int i = 0; i < daysPerCycle; i++){
                //Make sure no class is repeated twice in the day
                List<Course> usedInDay = new List<Course>(); // Track Courses used in this [i,j]
        //Per Day
                for (int j = 0; j < periodsPerDay; j++){
                    //Make sure no teacher or group is repeated twice in the same period
                    List<string> groupsInPeriod = new List<string>();
                    List<string> teachersInPeriod = new List<string>();
        //Per Period
                    for (int k = 0; k < numGroups; k++){
                        //Make a list of available courses(not present in the day and not present "rep" times in the cycle)
                        List<Course> availableCourses = allCourses
                            .Where(c => courseCount[c] < c.repetitions && !usedInDay.Contains(c) && !groupsInPeriod.Contains(c.group) && !teachersInPeriod.Contains(c.teacher))
                            .ToList();

                        if (availableCourses.Count == 0)
                        {
                            Course empty = new Course(){};
                            empty.courseName = "Empty";
                            //schedule2dArray[i, j, k] = empty;
                        }
                        else {
                            List<Course> availableCoursesIteration = shuffleList(availableCourses);
                            for (int z=0; z<availableCoursesIteration.Count; z++){

                                //Find all with shared link
                                if(availableCoursesIteration[z].link!=null && availableCoursesIteration[z].link!=""){
                                    List<Course> linkedCourses = allCourses
                                        .Where(c => c.link == availableCoursesIteration[z].link)
                                        .ToList();

                                    if (linkedCourses.All(c1 => availableCourses.Any(c2 => c2.Equals(c1)))) {
                                        for (int y=0; y<linkedCourses.Count;y++){
                                            schedule2dArray[i,j,k] = linkedCourses[y];
                                            courseCount[linkedCourses[y]]++;
                                            usedInDay.Add(linkedCourses[y]);
                                            teachersInPeriod.Add(linkedCourses[y].teacher);
                                            groupsInPeriod.Add(linkedCourses[y].group);
                                            k++;
                                        }
                                        linkedCourses.ForEach(x=>{
                                            availableCourses.Remove(x);
                                        });
                                        
                                        break;
                                    }
                                }

                                //If no link -->
                                else {
                                    schedule2dArray[i, j, k] = availableCoursesIteration[z];
                                    courseCount[availableCoursesIteration[z]]++;
                                    usedInDay.Add(availableCoursesIteration[z]);
                                    teachersInPeriod.Add(availableCoursesIteration[z].teacher);
                                    groupsInPeriod.Add(availableCoursesIteration[z].group);
                                    availableCourses.Remove(availableCoursesIteration[z]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
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

    }
}