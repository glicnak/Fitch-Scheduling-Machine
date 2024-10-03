using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class ForcedCourses
    {
        public static Dictionary<Course,List<int[]>> placeCourses(List<Course> allCourses){

            Dictionary<Course,List<int[]>> forcedCourses = new Dictionary<Course,List<int[]>>(){};
            
            //Manually inputting the schedule:
            addForcedCourse("Out Ed A",0,0,allCourses,forcedCourses);
            addForcedCourse("Out Ed B",0,0,allCourses,forcedCourses);
            addForcedCourse("Out Ed C",0,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed D",0,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed E",0,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed F",0,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed G",0,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed NV",1,2,allCourses,forcedCourses); //flexible
            addForcedCourse("Out Ed JS",0,4,allCourses,forcedCourses); //flexible
            addForcedCourse("CCQ Sc4Girls D",0,1,allCourses,forcedCourses);
            addForcedCourse("CCQ Sc4Boys F",0,2,allCourses,forcedCourses);
            addForcedCourse("CCQ Sc4Boys G",0,2,allCourses,forcedCourses);

            addForcedCourse("Out Ed A",4,3,allCourses,forcedCourses);
            addForcedCourse("Out Ed B",4,3,allCourses,forcedCourses);
            addForcedCourse("Out Ed C",4,4,allCourses,forcedCourses);
            addForcedCourse("Out Ed D",4,4,allCourses,forcedCourses);
            addForcedCourse("Out Ed E",4,5,allCourses,forcedCourses);
            addForcedCourse("Out Ed F",4,5,allCourses,forcedCourses);
            addForcedCourse("Out Ed G",4,5,allCourses,forcedCourses);
            addForcedCourse("Out Ed NV",2,2,allCourses,forcedCourses); //flexible
            addForcedCourse("Out Ed JS",1,4,allCourses,forcedCourses); //flexible
            addForcedCourse("CCQ Sc4Girls D",4,4,allCourses,forcedCourses);
            addForcedCourse("CCQ Sc4Boys F",4,5,allCourses,forcedCourses);
            addForcedCourse("CCQ Sc4Boys G",4,5,allCourses,forcedCourses);
            
            addForcedCourse("Out Ed Ex A",1,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex B",1,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex C",2,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex D",2,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex E",3,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex F",3,1,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex G",3,1,allCourses,forcedCourses);
            
            addForcedCourse("Out Ed Ex A",1,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex B",1,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex C",2,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex D",2,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex E",3,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex F",3,2,allCourses,forcedCourses);
            addForcedCourse("Out Ed Ex G",3,2,allCourses,forcedCourses);
            
            addForcedCourse("Work1 WOTP PM",0,0,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP PM",1,0,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP PM",2,0,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP PM",3,0,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP PM",4,0,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP PM",0,1,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP PM",1,1,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP PM",2,1,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP PM",3,1,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP PM",4,1,allCourses,forcedCourses);
            addForcedCourse("Work3 WOTP PM",1,2,allCourses,forcedCourses);
            addForcedCourse("Work3 WOTP PM",3,2,allCourses,forcedCourses);
            
            addForcedCourse("Work1 WOTP AM",0,5,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP AM",1,5,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP AM",2,5,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP AM",3,5,allCourses,forcedCourses);
            addForcedCourse("Work1 WOTP AM",4,5,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP AM",0,4,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP AM",1,4,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP AM",2,4,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP AM",3,4,allCourses,forcedCourses);
            addForcedCourse("Work2 WOTP AM",4,4,allCourses,forcedCourses);
            addForcedCourse("Work3 WOTP AM",0,3,allCourses,forcedCourses);
            addForcedCourse("Work3 WOTP AM",2,3,allCourses,forcedCourses);

            //Nick
            addForcedCourse("CCQ NV",0,0,allCourses,forcedCourses);
            addForcedCourse("CCQ NV",1,1,allCourses,forcedCourses);
            addForcedCourse("CCQ JS",0,1,allCourses,forcedCourses);
            addForcedCourse("CCQ JS",1,0,allCourses,forcedCourses);
            addForcedCourse("Music NV",2,1,allCourses,forcedCourses);
            addForcedCourse("Music NV",3,0,allCourses,forcedCourses);
            addForcedCourse("Music JS",2,0,allCourses,forcedCourses);
            addForcedCourse("Music JS",3,1,allCourses,forcedCourses);

            //Phys Ed Ideal
            addForcedCourse("Phys Ed A",2,0,allCourses,forcedCourses);
            addForcedCourse("Phys Ed A",3,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed B",2,0,allCourses,forcedCourses);
            addForcedCourse("Phys Ed B",3,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed C",1,0,allCourses,forcedCourses);
            addForcedCourse("Phys Ed C",3,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed D",1,0,allCourses,forcedCourses);
            addForcedCourse("Phys Ed D",3,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed E",1,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed E",2,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed F",1,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed F",2,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed G",1,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed G",2,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed WOTP AM",3,0,allCourses,forcedCourses);
            addForcedCourse("Phys Ed WOTP AM",4,2,allCourses,forcedCourses);
            addForcedCourse("Phys Ed WOTP PM",0,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed WOTP PM",1,4,allCourses,forcedCourses);
            addForcedCourse("Phys Ed NV",0,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed NV",4,0,allCourses,forcedCourses);
            addForcedCourse("Phys Ed JS",2,3,allCourses,forcedCourses);
            addForcedCourse("Phys Ed JS",4,1,allCourses,forcedCourses);

            //Phys Ed Old
            // addForcedCourse("Phys Ed A",2,0,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed A",3,4,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed B",2,0,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed B",3,4,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed C",1,0,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed C",3,5,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed D",1,0,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed D",3,5,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed E",1,3,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed E",2,5,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed F",1,3,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed F",2,5,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed G",1,3,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed G",2,5,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed WOTP AM",3,0,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed WOTP AM",4,2,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed WOTP PM",0,4,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed WOTP PM",1,5,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed NV",0,3,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed NV",4,0,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed JS",2,4,allCourses,forcedCourses);
            // addForcedCourse("Phys Ed JS",4,1,allCourses,forcedCourses);

            //Mike
            // addForcedCourse("Art A",0,3,allCourses,forcedCourses);
            // addForcedCourse("Art A",2,2,allCourses,forcedCourses);
            // addForcedCourse("Music B",0,3,allCourses,forcedCourses);
            // addForcedCourse("Music B",2,2,allCourses,forcedCourses);
            // addForcedCourse("Art C",0,2,allCourses,forcedCourses);
            // addForcedCourse("Art C",1,2,allCourses,forcedCourses);
            // addForcedCourse("Music D",0,2,allCourses,forcedCourses);
            // addForcedCourse("Music D",1,2,allCourses,forcedCourses);
            // addForcedCourse("Art E",1,1,allCourses,forcedCourses);
            // addForcedCourse("Art E",2,1,allCourses,forcedCourses);
            // addForcedCourse("Music F",1,1,allCourses,forcedCourses);
            // addForcedCourse("Music F",2,1,allCourses,forcedCourses);
            // addForcedCourse("Music G",1,1,allCourses,forcedCourses);
            // addForcedCourse("Music G",2,1,allCourses,forcedCourses);
            // addForcedCourse("Science NV",0,1,allCourses,forcedCourses);
            // addForcedCourse("Science NV",1,3,allCourses,forcedCourses);
            // addForcedCourse("Science NV",2,3,allCourses,forcedCourses);
            // addForcedCourse("Science NV",4,1,allCourses,forcedCourses);
            // addForcedCourse("Science JS",0,5,allCourses,forcedCourses);
            // addForcedCourse("Science JS",1,5,allCourses,forcedCourses);
            // addForcedCourse("Science JS",2,5,allCourses,forcedCourses);
            // addForcedCourse("Science JS",4,0,allCourses,forcedCourses);

            return forcedCourses;
        }

        static void addForcedCourse(string name, int day, int period, List<Course> allCourses, Dictionary<Course,List<int[]>> forcedCourses){
            //Find the course
            Course course = allCourses.Find(c => c.courseName == name);
            // Check if the key exists
            if (course!=null && forcedCourses.ContainsKey(course)){
                // If the key exists, add the value to the existing value
                forcedCourses[course].Add(new int[] {day,period});
                //Console.WriteLine("added: " + course.courseName + " to day " + (day+1) + " and period " + (period+1));
            }
            else{
                // If the key does not exist, add the key with the value
                forcedCourses.Add(course, new List<int[]> {new int[]{day,period}});
                //Console.WriteLine("added: " + course.courseName + " to day " + (day+1) + " and period " + (period+1));
            }
        }
    }
}