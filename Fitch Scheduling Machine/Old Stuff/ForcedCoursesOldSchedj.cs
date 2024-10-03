using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class ForcedCoursesOldSchedj
    {
        public static Dictionary<Course,List<int[]>> placeCourses(List<Course> allCourses){

            Dictionary<Course,List<int[]>> forcedCourses = new Dictionary<Course,List<int[]>>(){};

            int day;
            int period;
            day = 0;//Day 1
            period = 0;//Period 1
                addForcedCourse("Out Ed A",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed B",day,period,allCourses,forcedCourses);
                addForcedCourse("Math C",day,period,allCourses,forcedCourses);
                addForcedCourse("English D",day,period,allCourses,forcedCourses);
                addForcedCourse("Science E",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ F",day,period,allCourses,forcedCourses);
                addForcedCourse("Math G",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Art JS",day,period,allCourses,forcedCourses);
                addForcedCourse("French WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP PM",day,period,allCourses,forcedCourses);
            period = 1; //Period 2
                addForcedCourse("Science A",day,period,allCourses,forcedCourses);
                addForcedCourse("French B",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed C",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed D",day,period,allCourses,forcedCourses);
                addForcedCourse("History E",day,period,allCourses,forcedCourses);
                addForcedCourse("English F",day,period,allCourses,forcedCourses);
                addForcedCourse("Math G",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ Sc4Girls D",day,period,allCourses,forcedCourses);
                addForcedCourse("Art NV",day,period,allCourses,forcedCourses);
                addForcedCourse("English JS",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP PM",day,period,allCourses,forcedCourses);
            period = 2; //Period 3
                addForcedCourse("Math A",day,period,allCourses,forcedCourses);
                addForcedCourse("History B",day,period,allCourses,forcedCourses);
                addForcedCourse("Science C",day,period,allCourses,forcedCourses);
                addForcedCourse("English D",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed E",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed F",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed G",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ Sc4Boys F",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ Sc4Boys G",day,period,allCourses,forcedCourses);
                addForcedCourse("History NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Science JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Job Market WOTP PM",day,period,allCourses,forcedCourses);
            period = 3; //Period 4
                addForcedCourse("French A",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ B",day,period,allCourses,forcedCourses);
                addForcedCourse("Art C",day,period,allCourses,forcedCourses);
                addForcedCourse("Music D",day,period,allCourses,forcedCourses);
                addForcedCourse("Science E",day,period,allCourses,forcedCourses);
                addForcedCourse("Math F",day,period,allCourses,forcedCourses);
                addForcedCourse("French G",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed NV",day,period,allCourses,forcedCourses);
                addForcedCourse("French JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP PM",day,period,allCourses,forcedCourses);
            period = 4; //Period 5
                addForcedCourse("English A",day,period,allCourses,forcedCourses);
                addForcedCourse("Math B",day,period,allCourses,forcedCourses);
                addForcedCourse("History C",day,period,allCourses,forcedCourses);
                addForcedCourse("Math D",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ E",day,period,allCourses,forcedCourses);
                addForcedCourse("French F",day,period,allCourses,forcedCourses);
                addForcedCourse("English G",day,period,allCourses,forcedCourses);
                addForcedCourse("French NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed WOTP PM",day,period,allCourses,forcedCourses);
            period = 5; //Period 6
                addForcedCourse("Art A",day,period,allCourses,forcedCourses);
                addForcedCourse("Music B",day,period,allCourses,forcedCourses);
                addForcedCourse("English C",day,period,allCourses,forcedCourses);
                addForcedCourse("History D",day,period,allCourses,forcedCourses);
                addForcedCourse("CW D",day,period,allCourses,forcedCourses);
                addForcedCourse("French E",day,period,allCourses,forcedCourses);
                addForcedCourse("Science F",day,period,allCourses,forcedCourses);
                addForcedCourse("Science G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech F",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech G",day,period,allCourses,forcedCourses);
                addForcedCourse("English NV",day,period,allCourses,forcedCourses);
                addForcedCourse("History JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work3 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("English2 WOTP PM",day,period,allCourses,forcedCourses);
            day = 1; // Day 2
            period = 0; //Period 1
                addForcedCourse("History A",day,period,allCourses,forcedCourses);
                addForcedCourse("English B",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed C",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed D",day,period,allCourses,forcedCourses);
                addForcedCourse("French E",day,period,allCourses,forcedCourses);
                addForcedCourse("Science F",day,period,allCourses,forcedCourses);
                addForcedCourse("Science G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech F",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech G",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ NV",day,period,allCourses,forcedCourses);
                addForcedCourse("History JS",day,period,allCourses,forcedCourses);
                addForcedCourse("English2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP PM",day,period,allCourses,forcedCourses);
            period = 1; //Period 2
                addForcedCourse("Out Ed Ex A",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex B",day,period,allCourses,forcedCourses);
                addForcedCourse("History C",day,period,allCourses,forcedCourses);
                addForcedCourse("Math D",day,period,allCourses,forcedCourses);
                addForcedCourse("Science E",day,period,allCourses,forcedCourses);
                addForcedCourse("English F",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ G",day,period,allCourses,forcedCourses);
                addForcedCourse("English NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Math JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP PM",day,period,allCourses,forcedCourses);
            period = 2; //Period 3
                addForcedCourse("Out Ed Ex A",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex B",day,period,allCourses,forcedCourses);
                addForcedCourse("English C",day,period,allCourses,forcedCourses);
                addForcedCourse("Science D",day,period,allCourses,forcedCourses);
                addForcedCourse("Art E",day,period,allCourses,forcedCourses);
                addForcedCourse("Music F",day,period,allCourses,forcedCourses);
                addForcedCourse("Music G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech D",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed NV",day,period,allCourses,forcedCourses);
                addForcedCourse("French JS",day,period,allCourses,forcedCourses);
                addForcedCourse("English2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work3 WOTP PM",day,period,allCourses,forcedCourses);
            period = 3; //Period 4
                addForcedCourse("History A",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ B",day,period,allCourses,forcedCourses);
                addForcedCourse("Math C",day,period,allCourses,forcedCourses);
                addForcedCourse("French D",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed E",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed F",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed G",day,period,allCourses,forcedCourses);
                addForcedCourse("Math NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Math JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Job Market WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("French WOTP PM",day,period,allCourses,forcedCourses);
            period = 4; //Period 5
                addForcedCourse("English A",day,period,allCourses,forcedCourses);
                addForcedCourse("Science B",day,period,allCourses,forcedCourses);
                addForcedCourse("English C",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ D",day,period,allCourses,forcedCourses);
                addForcedCourse("History E",day,period,allCourses,forcedCourses);
                addForcedCourse("French F",day,period,allCourses,forcedCourses);
                addForcedCourse("Math G",day,period,allCourses,forcedCourses);
                addForcedCourse("English NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP PM",day,period,allCourses,forcedCourses);
            period = 5; //Period 6
                addForcedCourse("CCQ A",day,period,allCourses,forcedCourses);
                addForcedCourse("French B",day,period,allCourses,forcedCourses);
                addForcedCourse("French C",day,period,allCourses,forcedCourses);
                addForcedCourse("Science D",day,period,allCourses,forcedCourses);
                addForcedCourse("English E",day,period,allCourses,forcedCourses);
                addForcedCourse("History F",day,period,allCourses,forcedCourses);
                addForcedCourse("History G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech D",day,period,allCourses,forcedCourses);
                addForcedCourse("CW F",day,period,allCourses,forcedCourses);
                addForcedCourse("CW G",day,period,allCourses,forcedCourses);
                addForcedCourse("History NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Math JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed WOTP PM",day,period,allCourses,forcedCourses);
            day = 2; // Day 3
            period = 0; //Period 1
                addForcedCourse("Phys Ed A",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed B",day,period,allCourses,forcedCourses);
                addForcedCourse("French C",day,period,allCourses,forcedCourses);
                addForcedCourse("Science D",day,period,allCourses,forcedCourses);
                addForcedCourse("History E",day,period,allCourses,forcedCourses);
                addForcedCourse("English F",day,period,allCourses,forcedCourses);
                addForcedCourse("French G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech D",day,period,allCourses,forcedCourses);
                addForcedCourse("English NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Music JS",day,period,allCourses,forcedCourses);
                addForcedCourse("French WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP PM",day,period,allCourses,forcedCourses);
            period = 1; //Period 2
                addForcedCourse("French A",day,period,allCourses,forcedCourses);
                addForcedCourse("English B",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex C",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex D",day,period,allCourses,forcedCourses);
                addForcedCourse("Math E",day,period,allCourses,forcedCourses);
                addForcedCourse("History F",day,period,allCourses,forcedCourses);
                addForcedCourse("History G",day,period,allCourses,forcedCourses);
                addForcedCourse("CW F",day,period,allCourses,forcedCourses);
                addForcedCourse("CW G",day,period,allCourses,forcedCourses);
                addForcedCourse("Music NV",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ JS",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP PM",day,period,allCourses,forcedCourses);
            period = 2; //Period 3
                addForcedCourse("Science A",day,period,allCourses,forcedCourses);
                addForcedCourse("Math B",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex C",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex D",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ E",day,period,allCourses,forcedCourses);
                addForcedCourse("Math F",day,period,allCourses,forcedCourses);
                addForcedCourse("English G",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Science JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP PM",day,period,allCourses,forcedCourses);
            period = 3; //Period 4
                addForcedCourse("History A",day,period,allCourses,forcedCourses);
                addForcedCourse("Science B",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ C",day,period,allCourses,forcedCourses);
                addForcedCourse("English D",day,period,allCourses,forcedCourses);
                addForcedCourse("Art E",day,period,allCourses,forcedCourses);
                addForcedCourse("Music F",day,period,allCourses,forcedCourses);
                addForcedCourse("Music G",day,period,allCourses,forcedCourses);
                addForcedCourse("French NV",day,period,allCourses,forcedCourses);
                addForcedCourse("English JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Job Market WOTP PM",day,period,allCourses,forcedCourses);
            period = 4; //Period 5
                addForcedCourse("Math A",day,period,allCourses,forcedCourses);
                addForcedCourse("French B",day,period,allCourses,forcedCourses);
                addForcedCourse("Science C",day,period,allCourses,forcedCourses);
                addForcedCourse("History D",day,period,allCourses,forcedCourses);
                addForcedCourse("Math E",day,period,allCourses,forcedCourses);
                addForcedCourse("French F",day,period,allCourses,forcedCourses);
                addForcedCourse("English G",day,period,allCourses,forcedCourses);
                addForcedCourse("CW D",day,period,allCourses,forcedCourses);
                addForcedCourse("Math NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("English2 WOTP PM",day,period,allCourses,forcedCourses);
            period = 5; //Period 6
                addForcedCourse("Science A",day,period,allCourses,forcedCourses);
                addForcedCourse("History B",day,period,allCourses,forcedCourses);
                addForcedCourse("French C",day,period,allCourses,forcedCourses);
                addForcedCourse("French D",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed E",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed F",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed G",day,period,allCourses,forcedCourses);
                addForcedCourse("Science NV",day,period,allCourses,forcedCourses);
                addForcedCourse("French JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work3 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP PM",day,period,allCourses,forcedCourses);
            day = 3; // Day 4
            period = 0; //Period 1
                addForcedCourse("French A",day,period,allCourses,forcedCourses);
                addForcedCourse("History B",day,period,allCourses,forcedCourses);
                addForcedCourse("Math C",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ D",day,period,allCourses,forcedCourses);
                addForcedCourse("French E",day,period,allCourses,forcedCourses);
                addForcedCourse("Science F",day,period,allCourses,forcedCourses);
                addForcedCourse("Science G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech F",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech G",day,period,allCourses,forcedCourses);
                addForcedCourse("Music NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Art JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP PM",day,period,allCourses,forcedCourses);
            period = 1; //Period 2
                addForcedCourse("Science A",day,period,allCourses,forcedCourses);
                addForcedCourse("Math B",day,period,allCourses,forcedCourses);
                addForcedCourse("English C",day,period,allCourses,forcedCourses);
                addForcedCourse("History D",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex E",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex F",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex G",day,period,allCourses,forcedCourses);
                addForcedCourse("CW D",day,period,allCourses,forcedCourses);
                addForcedCourse("History NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Music JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP PM",day,period,allCourses,forcedCourses);
            period = 2; //Period 3
                addForcedCourse("History A",day,period,allCourses,forcedCourses);
                addForcedCourse("English B",day,period,allCourses,forcedCourses);
                addForcedCourse("Science C",day,period,allCourses,forcedCourses);
                addForcedCourse("Math D",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex E",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex F",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed Ex G",day,period,allCourses,forcedCourses);
                addForcedCourse("Math NV",day,period,allCourses,forcedCourses);
                addForcedCourse("History JS",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work3 WOTP PM",day,period,allCourses,forcedCourses);
            period = 3; //Period 4
                addForcedCourse("Math A",day,period,allCourses,forcedCourses);
                addForcedCourse("Science B",day,period,allCourses,forcedCourses);
                addForcedCourse("French C",day,period,allCourses,forcedCourses);
                addForcedCourse("English D",day,period,allCourses,forcedCourses);
                addForcedCourse("History E",day,period,allCourses,forcedCourses);
                addForcedCourse("Math F",day,period,allCourses,forcedCourses);
                addForcedCourse("French G",day,period,allCourses,forcedCourses);
                addForcedCourse("Science NV",day,period,allCourses,forcedCourses);
                addForcedCourse("French JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Job Market WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP PM",day,period,allCourses,forcedCourses);
            period = 4; //Period 5
                addForcedCourse("Phys Ed A",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed B",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ C",day,period,allCourses,forcedCourses);
                addForcedCourse("French D",day,period,allCourses,forcedCourses);
                addForcedCourse("Math E",day,period,allCourses,forcedCourses);
                addForcedCourse("History F",day,period,allCourses,forcedCourses);
                addForcedCourse("History G",day,period,allCourses,forcedCourses);
                addForcedCourse("CW F",day,period,allCourses,forcedCourses);
                addForcedCourse("CW G",day,period,allCourses,forcedCourses);
                addForcedCourse("French NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Science JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP PM",day,period,allCourses,forcedCourses);
            period = 5; //Period 6
                addForcedCourse("Art A",day,period,allCourses,forcedCourses);
                addForcedCourse("Music B",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed C",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed D",day,period,allCourses,forcedCourses);
                addForcedCourse("English E",day,period,allCourses,forcedCourses);
                addForcedCourse("Math F",day,period,allCourses,forcedCourses);
                addForcedCourse("French G",day,period,allCourses,forcedCourses);
                addForcedCourse("History NV",day,period,allCourses,forcedCourses);
                addForcedCourse("English JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Job Market WOTP PM",day,period,allCourses,forcedCourses);
            day = 4; // Day 5
            period = 0; //Period 1
                addForcedCourse("English A",day,period,allCourses,forcedCourses);
                addForcedCourse("History B",day,period,allCourses,forcedCourses);
                addForcedCourse("Science C",day,period,allCourses,forcedCourses);
                addForcedCourse("French D",day,period,allCourses,forcedCourses);
                addForcedCourse("Math E",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ F",day,period,allCourses,forcedCourses);
                addForcedCourse("English G",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed NV",day,period,allCourses,forcedCourses);
                addForcedCourse("English JS",day,period,allCourses,forcedCourses);
                addForcedCourse("French WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP PM",day,period,allCourses,forcedCourses);
            period = 1; //Period 2
                addForcedCourse("Math A",day,period,allCourses,forcedCourses);
                addForcedCourse("English B",day,period,allCourses,forcedCourses);
                addForcedCourse("Art C",day,period,allCourses,forcedCourses);
                addForcedCourse("Music D",day,period,allCourses,forcedCourses);
                addForcedCourse("French E",day,period,allCourses,forcedCourses);
                addForcedCourse("Science F",day,period,allCourses,forcedCourses);
                addForcedCourse("Science G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech F",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech G",day,period,allCourses,forcedCourses);
                addForcedCourse("French NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed JS",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP PM",day,period,allCourses,forcedCourses);
             period = 2; //Period 3
                addForcedCourse("English A",day,period,allCourses,forcedCourses);
                addForcedCourse("French B",day,period,allCourses,forcedCourses);
                addForcedCourse("History C",day,period,allCourses,forcedCourses);
                addForcedCourse("Math D",day,period,allCourses,forcedCourses);
                addForcedCourse("English E",day,period,allCourses,forcedCourses);
                addForcedCourse("French F",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ G",day,period,allCourses,forcedCourses);
                addForcedCourse("Science NV",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Phys Ed WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("French WOTP PM",day,period,allCourses,forcedCourses);
             period = 3; //Period 4
                addForcedCourse("Out Ed A",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed B",day,period,allCourses,forcedCourses);
                addForcedCourse("Math C",day,period,allCourses,forcedCourses);
                addForcedCourse("History D",day,period,allCourses,forcedCourses);
                addForcedCourse("Science E",day,period,allCourses,forcedCourses);
                addForcedCourse("English F",day,period,allCourses,forcedCourses);
                addForcedCourse("Math G",day,period,allCourses,forcedCourses);
                addForcedCourse("CW D",day,period,allCourses,forcedCourses);
                addForcedCourse("Math NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Math JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Job Market WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("French WOTP PM",day,period,allCourses,forcedCourses);
             period = 4; //Period 5
                addForcedCourse("French A",day,period,allCourses,forcedCourses);
                addForcedCourse("Science B",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed C",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed D",day,period,allCourses,forcedCourses);
                addForcedCourse("English E",day,period,allCourses,forcedCourses);
                addForcedCourse("History F",day,period,allCourses,forcedCourses);
                addForcedCourse("History G",day,period,allCourses,forcedCourses);
                addForcedCourse("CW F",day,period,allCourses,forcedCourses);
                addForcedCourse("CW G",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ Sc4Girls D",day,period,allCourses,forcedCourses);
                addForcedCourse("Art NV",day,period,allCourses,forcedCourses);
                addForcedCourse("Science JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work1 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("English WOTP PM",day,period,allCourses,forcedCourses);
             period = 5; //Period 6
                addForcedCourse("CCQ A",day,period,allCourses,forcedCourses);
                addForcedCourse("Math B",day,period,allCourses,forcedCourses);
                addForcedCourse("History C",day,period,allCourses,forcedCourses);
                addForcedCourse("Science D",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed E",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed F",day,period,allCourses,forcedCourses);
                addForcedCourse("Out Ed G",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ Sc4Boys F",day,period,allCourses,forcedCourses);
                addForcedCourse("CCQ Sc4Boys G",day,period,allCourses,forcedCourses);
                addForcedCourse("Tech D",day,period,allCourses,forcedCourses);
                addForcedCourse("Science NV",day,period,allCourses,forcedCourses);
                addForcedCourse("History JS",day,period,allCourses,forcedCourses);
                addForcedCourse("Work2 WOTP AM",day,period,allCourses,forcedCourses);
                addForcedCourse("Math WOTP PM",day,period,allCourses,forcedCourses);

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