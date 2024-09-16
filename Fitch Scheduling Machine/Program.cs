// See https://aka.ms/new-console-template for more information
using Fitch_Scheduling_Machine;

//Initialize variables
List<Course> allCourses = new List<Course>(MakeCourseList.makeCourseList());

int totalNumberOfClasses=0;
//Print out Names of all Courses
allCourses.ForEach(x => {
    Console.WriteLine(x.courseName);
    totalNumberOfClasses+=x.repetitions;
});

Console.WriteLine("Total number of classes: " + totalNumberOfClasses);

MakeSchedule.makeSchedule(allCourses);

