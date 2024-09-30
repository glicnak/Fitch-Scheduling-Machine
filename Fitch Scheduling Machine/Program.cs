// See https://aka.ms/new-console-template for more information
using Fitch_Scheduling_Machine;

//Initialize variables
List<Course> allCourses = new List<Course>(MakeCourseList.makeCourseList());

Course[,,] array3dCourses = CreateSchedule.makeSchedule(allCourses);
string[,,] array3dStrings = new string[array3dCourses.GetLength(0),array3dCourses.GetLength(1),array3dCourses.GetLength(2)];

for (int i =0;i<array3dCourses.GetLength(0);i++){
    for (int j = 0; j<array3dCourses.GetLength(1);j++){
        for (int k = 0; k<array3dCourses.GetLength(2);k++){
            if(array3dCourses[i,j,k] != null){
                array3dStrings[i,j,k] = "" + array3dCourses[i,j,k].courseName + " " + array3dCourses[i,j,k].teacher;
            }
        }
    }
}

PrintExcel.Main(array3dStrings);
