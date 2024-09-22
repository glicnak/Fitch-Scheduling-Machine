using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class ForcedCourses
    {
        public static Dictionary<Course,List<int[]>> placeCourses(List<Course> allCourses){

            Dictionary<Course,List<int[]>> forcedCourses = new Dictionary<Course,List<int[]>>(){
                {allCourses.Find(c => c.courseName == "Out Ed A"), new List<int[]> {new int[] {0,0}, new int[] {4,3}}},
                {allCourses.Find(c => c.courseName == "Out Ed B"), new List<int[]> {new int[] {0,0}, new int[] {4,3}}},
                {allCourses.Find(c => c.courseName == "Out Ed C"), new List<int[]> {new int[] {0,1}, new int[] {4,4}}},
                {allCourses.Find(c => c.courseName == "Out Ed D"), new List<int[]> {new int[] {0,1}, new int[] {4,4}}},
                {allCourses.Find(c => c.courseName == "Out Ed E"), new List<int[]> {new int[] {0,2}, new int[] {4,5}}},
                {allCourses.Find(c => c.courseName == "Out Ed F"), new List<int[]> {new int[] {0,2}, new int[] {4,5}}},
                {allCourses.Find(c => c.courseName == "Out Ed G"), new List<int[]> {new int[] {0,2}, new int[] {4,5}}},
                
                {allCourses.Find(c => c.courseName == "Out Ed Ex A"), new List<int[]> {new int[] {1,1}, new int[] {1,2}}},
                {allCourses.Find(c => c.courseName == "Out Ed Ex B"), new List<int[]> {new int[] {1,1}, new int[] {1,2}}},
                {allCourses.Find(c => c.courseName == "Out Ed Ex C"), new List<int[]> {new int[] {2,1}, new int[] {2,2}}},
                {allCourses.Find(c => c.courseName == "Out Ed Ex D"), new List<int[]> {new int[] {2,1}, new int[] {2,2}}},
                {allCourses.Find(c => c.courseName == "Out Ed Ex E"), new List<int[]> {new int[] {3,1}, new int[] {3,2}}},
                {allCourses.Find(c => c.courseName == "Out Ed Ex F"), new List<int[]> {new int[] {3,1}, new int[] {3,2}}},
                {allCourses.Find(c => c.courseName == "Out Ed Ex G"), new List<int[]> {new int[] {3,1}, new int[] {3,2}}},

                {allCourses.Find(c => c.courseName == "Work1 WOTP AM"), new List<int[]> {new int[] {0,3}, new int[] {2,3}}},
                {allCourses.Find(c => c.courseName == "Work2 WOTP AM"), new List<int[]> {new int[] {0,4}, new int[] {1,4}, new int[] {2,4}, new int[] {3,4}, new int[] {4,4}}},
                {allCourses.Find(c => c.courseName == "Work3 WOTP AM"), new List<int[]> {new int[] {0,5}, new int[] {1,5}, new int[] {2,5}, new int[] {3,5}, new int[] {4,5}}},

                {allCourses.Find(c => c.courseName == "Work1 WOTP PM"), new List<int[]> {new int[] {0,0}, new int[] {1,0}, new int[] {2,0}, new int[] {3,0}, new int[] {4,0}}},
                {allCourses.Find(c => c.courseName == "Work2 WOTP PM"), new List<int[]> {new int[] {0,1}, new int[] {1,1}, new int[] {2,1}, new int[] {3,1}, new int[] {4,1}}},
                {allCourses.Find(c => c.courseName == "Work3 WOTP PM"), new List<int[]> {new int[] {1,2}, new int[] {3,2}}}
            };
            return forcedCourses;
        }
    }
}