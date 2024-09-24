// See https://aka.ms/new-console-template for more information
using Fitch_Scheduling_Machine;

//Initialize variables
List<Course> allCourses = new List<Course>(MakeCourseList.makeCourseList());

//Print out Names of all Courses
/*allCourses.ForEach(x => {
    Console.WriteLine(x.courseName + x.repetitions);
});*/

Course[,,] schedule3dArray = CreateSchedule.makeSchedule(allCourses);

//Convert schedule3dArray to string[,,]
    string[,,] schedule3dArrayStrings = new string[schedule3dArray.GetLength(0), schedule3dArray.GetLength(1), schedule3dArray.GetLength(2)];
    for (int i = 0; i < schedule3dArray.GetLength(0); i++){
        for (int j = 0; j < schedule3dArray.GetLength(1); j++){
            for (int k = 0; k < schedule3dArray.GetLength(2); k++){
                if(schedule3dArray[i, j, k]!=null){
                    schedule3dArrayStrings[i, j, k] = schedule3dArray[i, j, k].courseName;
                }
            }
        }
    }

//PrintExcel.Main(schedule3dArrayStrings);


