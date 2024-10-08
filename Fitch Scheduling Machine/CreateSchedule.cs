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
            Dictionary<string,int> teacherCount = new Dictionary<string,int>();
            foreach (Course c in allCourses){
                courseCount[c] = c.repetitions;
                
                //If the teacher is already in the dictionary, increment the count
                if (teacherCount.ContainsKey(c.teacher)){
                    teacherCount[c.teacher]+=c.repetitions;
                }
                // If the teacher is not in the dictionary, add them with a count of 1
                else{
                    teacherCount.Add(c.teacher, c.repetitions);
                }
            }



            //Add the forced Classes
            addForcedCourses(schedule3dArray,ForcedCourses.placeCourses(allCourses),courseCount, teacherCount);

            //List of all courses available
            List<Course> availableCourses = courseCount
                .Where(entry => entry.Value > 0)
                .Select(entry => entry.Key)
                .ToList();

            //List of courses used in a day
            List<Course> coursesUsedInDay = new List<Course>();

            //List of groups used in a period
            List<string> groupsUsedInPeriod = new List<string>();

            //TEACHER CHECK UPDATE 51-53
            //List of teachers used in a period
            List<string> teachersUsedInPeriod = new List<string>();
                
            //update starting rank, available Courses and groups used by looking forward into the first period
            int z =-1;
            for(int i=0;i<numGroups;i++){
                if(schedule3dArray[0,0,i] != null){
                    z++;
                    if(!coursesUsedInDay.Contains(schedule3dArray[0,0,i])){
                        groupsUsedInPeriod.Add(schedule3dArray[0,0,i].group);
                        removeAssociatedCourses(schedule3dArray,allCourses,schedule3dArray[0,0,i],availableCourses,coursesUsedInDay, groupsUsedInPeriod, teachersUsedInPeriod, courseCount, teacherCount, daysPerCycle, periodsPerDay,0,0,numGroups);
                //TEACHER CHECK UPDATE 64-66
                        teachersUsedInPeriod.Add(schedule3dArray[0,0,i].teacher);
                    }
                }
            }

            //Populate the schedule             //TEACHER CHECK UPDATE 72 teacher
            populateSchedule(schedule3dArray, allCourses, allGroups, courseCount, teacherCount, availableCourses, groupsUsedInPeriod, teachersUsedInPeriod, coursesUsedInDay, daysPerCycle, periodsPerDay, numGroups,0,0,z);

            //Debug print extra or underused courses
            foreach(var entry in courseCount){
                if(entry.Value != 0){
                    Console.WriteLine("Over or under-used Course: " + entry.Key.courseName + " " + entry.Value + " " + entry.Key.teacher);
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

//*******************************************************************************************************************************
        public static List<Course> shuffleListByGroup(List<Course> list, Dictionary<Course,int> courseCount, Dictionary<string, int> teacherCount){
            list.ForEach(x=>{x.teacherReps = teacherCount[x.teacher];});
            var shuffledCourses = shuffleList(list)            
                .GroupBy(course => course.group)
                .OrderBy(group => group.Count())
                .SelectMany(group => 
                    // Order each group: first by repetitions (highest first), 
                    // then by whether the course has a link (courses with links first)
                    group.OrderByDescending(course => courseCount[course])
                        .ThenByDescending(course => course.teacherReps)
                        .ThenByDescending(course => !string.IsNullOrEmpty(course.link))
                )
                .ToList();
            list = shuffledCourses;
            return shuffledCourses;
        }

        public static void addForcedCourses(Course[,,] schedule3dArray, Dictionary<Course,List<int[]>> forcedCourses, Dictionary<Course,int> courseCount, Dictionary<string,int> teacherCount){
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
                            teacherCount[entry.Key.teacher]--;

                            break; // Exit the z loop once the course is placed
                        }
                    }
                }
            }
        }

//            //TEACHER CHECK UPDATE 166
        public static bool populateSchedule(Course[,,] schedule3dArray, List<Course> allCourses, List<string> allGroups, Dictionary<Course, int> courseCount, Dictionary<string,int> teacherCount, List<Course> availableCoursesBackup, List<string> groupsUsedInPeriod, List<string> teachersUsedInPeriod, List<Course> coursesUsedInDay, int daysPerCycle, int periodsPerDay, int numGroups, int x, int y, int z){
            // Calculate next index
            int nextX = x;
            int nextY = y;
            int nextZ = z+1;       
            int sleepX = 13;
            int sleepY = 5;
            int sleepZ = 0;

            //New available courses to modify
            List<Course> availableCourses = new List<Course>();
            for (int i = 0; i<availableCoursesBackup.Count;i++){
                availableCourses.Add(availableCoursesBackup[i]);
            }

            List<string> teachersNeededToBeAdded = new List<string>();

            //Check for a change in period or day
            if (availableCourses.Count == 0){ //If there are no available courses, we're at the end of 1 period...
                //Check if every group is present at least once before moving to the next day
                if(!allGroups.All(group => groupsUsedInPeriod.Contains(group))){
                    //Debug
                    Console.Write("Not all groups were used in period. Groups Used: " + groupsUsedInPeriod.Count + ": ");
                    groupsUsedInPeriod.ForEach(c=>{Console.Write(c + ", ");});
                    Console.WriteLine("");
                    if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};
                    return false;
                }
                //Move to the next period
                nextZ = 0;
                nextY = y+1;
                if (nextY == periodsPerDay){ //If we're at the end of 1 day
                    //Go to next day
                    nextX = x+1;
                    nextY = 0;
                    coursesUsedInDay.Clear();//Reset courses used in a day at the start of the day    
                }
                availableCourses.Clear(); // Reset available courses based on usedInDay
                            //TEACHER CHECK UPDATE 204
                groupsUsedInPeriod.Clear();
                teachersUsedInPeriod.Clear();
                availableCourses = courseCount
                    .Where(entry => entry.Value > 0 && !coursesUsedInDay.Contains(entry.Key))
                    .Select(entry => entry.Key)
                    .ToList();

                //Check for end of cycle
                if (nextX == daysPerCycle){ // If we're at the end of 1 cycle
                    //Debug
                    Console.WriteLine("It Worked!");
                    return true;
                }

                //update available Courses and groups used by looking forward into the period
                for(int i=0;i<numGroups;i++){
                    if(schedule3dArray[nextX,nextY,i] != null){
                        nextZ++;
                        groupsUsedInPeriod.Add(schedule3dArray[nextX,nextY,i].group);
                                    //TEACHER CHECK UPDATE 225
                        teachersUsedInPeriod.Add(schedule3dArray[nextX,nextY,i].teacher);
                        if(!removeAssociatedCourses(schedule3dArray,allCourses,schedule3dArray[nextX,nextY,i],availableCourses,coursesUsedInDay, groupsUsedInPeriod, teachersUsedInPeriod, courseCount, teacherCount, daysPerCycle, periodsPerDay, nextX, nextY, numGroups)){
                            return false;
                        }
                        //Debug
                        Console.Write(schedule3dArray[nextX,nextY,i].courseName + ",");
                    }
                }
                //Debug
                Console.WriteLine(" Forced into day " + (nextX+1) + " period " + (nextY+1));
                if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};

                //Check if any teachers need to be added this period
                teachersNeededToBeAdded = teachersAvailablePeriods(schedule3dArray, nextX,nextY,daysPerCycle,periodsPerDay,courseCount,teacherCount,availableCourses,numGroups);
            }

            //Check if there are available courses for every group thats left, otherwise return false
            List<string> groupsLeft =  allGroups.Except(groupsUsedInPeriod).ToList();
            if(!groupsLeft.All(g => availableCourses.Any(c => c.group == g))){
                //Debug
                Console.WriteLine("There are no courses available for certain groups left. Groups Used: " + groupsUsedInPeriod.Count + ": ");
                //Console.Write("Available Courses: ");
                availableCourses.ForEach(c=>{Console.Write(c.courseName + ", ");});
                Console.WriteLine("");
                if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};
                return false;
            }

//            //TEACHER CHECK UPDATE 250-266
            //Check if there are enough unique teachers left for the remaining number of groups
            List<string> teachersList = allCourses
                .Select(course => course.teacher)
                .Distinct()
                .ToList();

            //List of unique teachers remaining in a period
            List<string> teachersRemainingPerPeriod = teachersList.Except(teachersUsedInPeriod).ToList();

            if(groupsLeft.Count > teachersRemainingPerPeriod.Count){
                //Debug
                Console.WriteLine("There are not enough teachers for the number of groups left. Groups Left: " + groupsLeft.Count + ": ");
                Console.WriteLine("");
                if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};
                return false;
                }

            //If there are still no available courses, do it all again
            if(availableCourses.Count==0){
                if(populateSchedule(schedule3dArray, allCourses, allGroups, courseCount, teacherCount, availableCourses, groupsUsedInPeriod, teachersUsedInPeriod, coursesUsedInDay, daysPerCycle, periodsPerDay, numGroups, nextX, nextY, nextZ)){
                    return true;
                }
            }
            
            List<Course> orderedAvailableCourses = shuffleListByGroup(availableCourses,courseCount, teacherCount);
            List<Course> newAvailableCourses= new List<Course>();
            availableCourses = orderedAvailableCourses;

            if(!teachersNeededToBeAdded.Any()){
                newAvailableCourses = orderedAvailableCourses
                    .OrderBy(course => !teachersNeededToBeAdded.Contains(course.teacher))
                    .ToList();
                 availableCourses = newAvailableCourses;
            }

            // Try placing each string in the current cell
            List<Course> availableMidLoop = new List<Course>();
            List<Course> iteratedCourses = new List<Course>();
            foreach (Course c in availableCourses)
            {
            
                //Check that there are at least as many days as courseCount left
                if(courseCount.Any(entry => entry.Key.repetitions <= daysPerCycle && entry.Value > daysPerCycle-nextX)){ 
                    //Debug
                    Console.WriteLine("Days left: " + (daysPerCycle-nextX));
                    foreach (var entry in courseCount){
                        Console.WriteLine($"Course: {entry.Key.courseName}, Count: {entry.Value}");
                    }
                    Console.WriteLine("At least one course has more reps left than days left");
                    return false;
                }

                //Check if there are no more available courses for an unused group
                groupsLeft =  allGroups.Except(groupsUsedInPeriod).ToList();

                availableMidLoop = availableCourses
                    .ToList(); 
                
                availableMidLoop.RemoveAll(course => iteratedCourses.Contains(course));

                foreach (string teacher in teachersNeededToBeAdded){
                    // If any teacher from teachersNeededToBeAdded is not found in any course, return false
                    if (!availableCourses.Any(g => g.teacher == teacher)){
                        return false; // A required teacher is missing
                    }
                }

                //Debug
                Console.WriteLine("Available Mid Loop: ");
                availableMidLoop.ForEach(f=>{Console.Write(f.courseName + ", ");});
                Console.WriteLine();

                if(!groupsLeft.All(g => availableMidLoop.Any(d => d.group == g))){
                    //Debug
                    Console.WriteLine("There are no courses available for certain groups left. Groups Used: " + groupsUsedInPeriod.Count + ": ");
                    Console.Write("Available Courses: ");
                    availableCourses.ForEach(c=>{Console.Write(c.courseName + ", ");});
                    Console.WriteLine("");
                    if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};
                    return false;
                }

                //If was iterated, skip
                if(iteratedCourses.Contains(c)){
                    continue;
                }

                //Backup courses used in a day and groups used in period
                List<Course> coursesUsedInDayBackup = new List<Course>();
                coursesUsedInDay.ForEach(d=>{coursesUsedInDayBackup.Add(d);});
                
                List<string> groupsUsedInPeriodBackup = new List<string>();
                groupsUsedInPeriod.ForEach(d=>{groupsUsedInPeriodBackup.Add(d);});
// TEACHER CHECK BACKUP 314
                List<string> teachersUsedInPeriodBackup = new List<string>();
                teachersUsedInPeriod.ForEach(d=>{teachersUsedInPeriodBackup.Add(d);});      

                //Make a list of all courses with the same link (or just the class itself if it doesn't have one)
                List<Course> linkedCourses = new List<Course>();
                if (c.link != null && c.link !=""){
                    availableCourses.ForEach(a=>{
                        if (c.link == a.link){
                            linkedCourses.Add(a);
                            iteratedCourses.Add(a);
                        }
                    });
                }
                else {
                    linkedCourses.Add(c);
                    iteratedCourses.Add(c);
                }

                //Add courses //TEACHER CHECK UPDATE 334
                if(!addCoursesToSchedule(schedule3dArray, allCourses, linkedCourses, daysPerCycle, periodsPerDay,numGroups,nextX,nextY,nextZ, courseCount, teacherCount, availableMidLoop, coursesUsedInDayBackup, groupsUsedInPeriodBackup, teachersUsedInPeriodBackup, teachersNeededToBeAdded)){
                    return false;
                }
                nextZ += linkedCourses.Count-1;
                //Debug
                Console.Write("Available Courses left: ");
                availableMidLoop.ForEach(d=>{Console.Write(d.courseName + ", ");});
                Console.WriteLine("");
                Console.WriteLine("");
                if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};
                
                // Recurse to the next cell //TEACHER CHECK UPDATE 346
                if (populateSchedule(schedule3dArray, allCourses, allGroups, courseCount, teacherCount, availableMidLoop, groupsUsedInPeriodBackup, teachersUsedInPeriodBackup, coursesUsedInDayBackup, daysPerCycle, periodsPerDay, numGroups, nextX, nextY, nextZ))
                {
                    return true; // Comment this if you want to see all configurations, not just the first one
                }

                // Backtrack //TEACHER CHECK UPDATE 351
                nextZ += -(linkedCourses.Count-1);
                removeCoursesFromSchedule(schedule3dArray,linkedCourses, nextX, nextY, nextZ, courseCount, teacherCount, coursesUsedInDayBackup, groupsUsedInPeriodBackup, teachersUsedInPeriodBackup);
                //Check if any teachers need to be added this period
                teachersNeededToBeAdded = teachersAvailablePeriods(schedule3dArray, nextX,nextY,daysPerCycle,periodsPerDay,courseCount,teacherCount,availableCourses,numGroups);
                
                //Debug
                Console.WriteLine("");
                Console.WriteLine("Backtrack to day " + (nextX+1) + " period " + (nextY+1) + " rank " + nextZ + "!");
                Console.WriteLine("");
            }

            //Return false if we reach the end
            //Debug
            Console.WriteLine("Ran through all courses");
            if(nextX >= sleepX && nextY>= sleepY && nextZ >= sleepZ){Thread.Sleep(2000);};
            return false;
        }
// TEACHER CHECK UPDATE 374
        public static bool removeAssociatedCourses (Course[,,]schedule3dArray, List<Course> allCourses, Course course, List<Course> availableCourses, List<Course> coursesUsedInDay, List<string> groupsUsedInPeriod, List<string> teachersUsedInPeriod, Dictionary<Course,int> courseCount, Dictionary<string,int>teacherCount, int daysPerCycle, int periodsPerDay, int currentDay, int currentPeriod, int numGroups){
            //Remove it from groups in period
            if(!groupsUsedInPeriod.Contains(course.group)){
                groupsUsedInPeriod.Add(course.group);
            }

            //Add to courses used in day OR add to courses used in day only if coursesLeft <= days left
            if(!coursesUsedInDay.Contains(course) && course.repetitions < daysPerCycle){
                coursesUsedInDay.Add(course);
                availableCourses.Remove(course);
            }

            //Remove all courses with the same group, teacher, room or link from available courses
            List<Course> associatedClasses = new List<Course>();
            bool returnValue = true;
            allCourses.ForEach(c=>{
                if(c.teacher == course.teacher || c.group == course.group){
                    int plusOneForPossiblePeriodToday = possiblePeriodToday(schedule3dArray,c,currentPeriod,currentDay,periodsPerDay,numGroups);
                    if(c.repetitions <= daysPerCycle && courseCount[c] > (daysPerCycle-currentDay+plusOneForPossiblePeriodToday)){  //Fail if there wouldn't be enough classes anymore as of next period and tomorrow
                        returnValue= false;
                    }
                    associatedClasses.Add(c);
                }
            });
            if(!returnValue){return returnValue;}
            for (int i=0; i<associatedClasses.Count; i++){
                for (int j=0; j<availableCourses.Count; j++){
                    if(associatedClasses[i] != null && associatedClasses[i].link!=null && associatedClasses[i].link!= "" && availableCourses[j] != null && availableCourses[j].link == associatedClasses[i].link){
                        int plusOneForPossiblePeriodToday = possiblePeriodToday(schedule3dArray,associatedClasses[i],currentPeriod,currentDay,periodsPerDay,numGroups);
                        if(associatedClasses[i].repetitions <= daysPerCycle && courseCount[associatedClasses[i]] > (daysPerCycle-currentDay+plusOneForPossiblePeriodToday)){  //Fail if there wouldn't be enough classes anymore as of next period and tomorrow
                            return false;
                        }
                    }
                }
                availableCourses.RemoveAll(d => d.link != null && d.link != "" && d.link == associatedClasses[i].link);
                availableCourses.Remove(associatedClasses[i]);
            }
            return true;
        }

        public static int possiblePeriodToday(Course[,,]schedule3dArray, Course course, int y, int x, int periodsPerDay, int numGroups){
            int counter = 0;
            for (int i=y+1;i<periodsPerDay;i++){
                for (int j=0; j<numGroups;j++){
                    if(schedule3dArray[x,i,j] != null && (schedule3dArray[x,i,j].group == course.group || schedule3dArray[x,i,j].teacher == course.teacher)){
                        counter++;
                        break;
                    }
                }
            }
            if(counter < (periodsPerDay-y-1)){
                return 0;
            }
            return 1;
        }

        public static bool addCoursesToSchedule (Course[,,]schedule3dArray, List<Course> allCourses, List<Course> linkedCourses, int daysPerCycle, int periodsPerDay, int numGroups, int x, int y, int z, Dictionary<Course, int> courseCount, Dictionary<string,int> teacherCount, List<Course> availableCourses, List<Course> coursesUsedInDay, List<string> groupsUsedInPeriod, List<string> teachersUsedInPeriod, List<string> teachersNeededToBeAdded){
            //Add courses to schedule, decrement course count, and add it to courses used in day and remove it from available courses
            for (int i=0; i<linkedCourses.Count; i++){
                if(!removeAssociatedCourses(schedule3dArray,allCourses,linkedCourses[i],availableCourses,coursesUsedInDay,groupsUsedInPeriod, teachersUsedInPeriod, courseCount, teacherCount, daysPerCycle,periodsPerDay,x,y,numGroups)){
                    return false;
                }
                schedule3dArray[x, y, z+i] = linkedCourses[i];
                Console.WriteLine("Added class " + linkedCourses[i].courseName + " to day " + (x+1) + " period " + (y+1) + " rank " +(z+i));
                courseCount[linkedCourses[i]]--;
                teacherCount[linkedCourses[i].teacher]--;
                teachersNeededToBeAdded.Remove(linkedCourses[i].teacher);
            }
            return true;
        }

    public static List<string> teachersAvailablePeriods(Course[,,] schedule3dArray, int x, int y, int daysPerCycle, int periodsPerDay, Dictionary<Course,int> courseCount, Dictionary<string, int> teacherCount, List<Course> availableCourses, int numGroups){
            List<string> problematicTeachers = new List<string>();
            availableCourses.ForEach(c=>{
                int periodsLeftToTeach = teacherCount[c.teacher];
                int periodsLeftInTotal = 0;
                int periodsTeacherAlreadyTeaching = 0;
                for (int j=y; j<periodsPerDay;j++){
                    //do the check for today
                    for(int k=0;k<numGroups;k++){
                        if (schedule3dArray[x,j,k]!=null && schedule3dArray[x,j,k].teacher == c.teacher){
                            periodsTeacherAlreadyTeaching++;
                            break;
                        }
                    }
                    periodsLeftInTotal++;
                }
                for(int i = x+1; i<daysPerCycle; i++){
                    for (int j=0; j<periodsPerDay;j++){
                        //do the check for tomorrow
                        for(int k=0;k<numGroups;k++){
                            if (schedule3dArray[i,j,k] != null && schedule3dArray[i,j,k].teacher == c.teacher){
                                periodsTeacherAlreadyTeaching++;
                                break;
                            }
                        }
                        periodsLeftInTotal++;
                    }
                }
                if(periodsLeftToTeach == periodsLeftInTotal-periodsTeacherAlreadyTeaching){
                    problematicTeachers.Add(c.teacher);
                }
            });
            
            return problematicTeachers;
        }
        public static bool teacherAvailablePeriods(Course[,,] schedule3dArray, int x, int y, int daysPerCycle, int periodsPerDay, Dictionary<Course,int> courseCount, Dictionary<string, int> teacherCount, Course course, int numGroups){
            int periodsLeftToTeach = teacherCount[course.teacher];
            int periodsLeftInTotal = 0;
            int periodsTeacherAlreadyTeaching = 0;
            for (int j=y+1; j<periodsPerDay;j++){
                //do the check for today
                for(int k=0;k<numGroups;k++){
                    if (schedule3dArray[x,j,k]!=null && schedule3dArray[x,j,k].teacher == course.teacher){
                        periodsTeacherAlreadyTeaching++;
                        break;
                    }
                }
                periodsLeftInTotal++;
            }
            for(int i = x+1; i<daysPerCycle; i++){
                for (int j=0; j<periodsPerDay;j++){
                    //do the check for tomorrow
                    for(int k=0;k<numGroups;k++){
                        if (schedule3dArray[i,j,k] != null && schedule3dArray[i,j,k].teacher == course.teacher){
                            periodsTeacherAlreadyTeaching++;
                            break;
                        }
                    }
                    periodsLeftInTotal++;
                }
            }
            if(periodsLeftToTeach > periodsLeftInTotal-periodsTeacherAlreadyTeaching){
                //Debug
                /*Console.WriteLine();
                Console.WriteLine("The problem is " + course.teacher + " has to teach " + periodsLeftToTeach + " in " + periodsLeftInTotal + " while being forced to teach " + periodsTeacherAlreadyTeaching);
                Console.WriteLine();
                Thread.Sleep(3000);*/
                return false;
            }
            return true;
        }

//TEACHER CHECK UPDATE 424
        public static void removeCoursesFromSchedule (Course[,,]schedule3dArray, List<Course> linkedCourses, int x, int y, int z, Dictionary<Course, int> courseCount, Dictionary<string, int> teacherCount, List<Course> coursesUsedInDay, List<string> groupsUsedInPeriod, List<string> teachersUsedInPeriod){
            //Remove courses from schedule, increment course count back up, and remove it from courses used in day and add it to available courses
            for (int i=0; i<linkedCourses.Count; i++){
                schedule3dArray[x, y, z+i] = null;
                courseCount[linkedCourses[i]]++;
                teacherCount[linkedCourses[i].teacher]++;
                coursesUsedInDay.Remove(linkedCourses[i]);
                groupsUsedInPeriod.Remove(linkedCourses[i].group);
//TEACHER CHECK UPDATE 432
                teachersUsedInPeriod.Remove(linkedCourses[i].group);
            }
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