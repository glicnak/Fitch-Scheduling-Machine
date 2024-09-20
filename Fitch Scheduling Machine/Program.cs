// See https://aka.ms/new-console-template for more information
using Fitch_Scheduling_Machine;

//Initialize variables
List<Course> allCourses = new List<Course>(MakeCourseList.makeCourseList());

//Print out Names of all Courses
/*allCourses.ForEach(x => {
    Console.WriteLine(x.courseName + x.repetitions);
});*/

MakeSchedule.makeSchedule(allCourses);
