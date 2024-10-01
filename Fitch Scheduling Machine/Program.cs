// See https://aka.ms/new-console-template for more information
using Fitch_Scheduling_Machine;

//Initialize variables
List<Course> allCourses = new List<Course>(MakeCourseList.makeCourseList());

Course[,,] array3dCourses = CreateSchedule.makeSchedule(allCourses);
string[,,] array3dStrings = new string[array3dCourses.GetLength(0),array3dCourses.GetLength(1),array3dCourses.GetLength(2)];

//Translate from Course to string
for (int i =0;i<array3dCourses.GetLength(0);i++){
    for (int j = 0; j<array3dCourses.GetLength(1);j++){

        //Alphabetical Order
        Course[] tempArray = new Course[array3dCourses.GetLength(2)];
        for (int k = 0; k<array3dCourses.GetLength(2);k++){
            if(array3dCourses[i,j,k] != null){
                tempArray[k] = array3dCourses[i,j,k];  
            }
        }
        Array.Sort(tempArray, (x,y) => {
            if(x==null && y==null){return 0;}
            else if(x==null){return -1;}
            else if(y==null){return 1;}
            else{
                return string.Compare(x.group, y.group);
            }
        });
        for (int k = 0; k<array3dCourses.GetLength(2);k++){
            if(tempArray[k]!= null){
                array3dCourses[i,j,k] = tempArray[k];   
                array3dStrings[i,j,k] = "" + array3dCourses[i,j,k].group + " " + array3dCourses[i,j,k].subject + " " + array3dCourses[i,j,k].teacher;
            }
        }
    }
}

PrintExcel.Main(array3dStrings);
