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
        int m=0;
        for (int k = 0; k<array3dCourses.GetLength(2);k++){
            if(tempArray[k]!= null){
                array3dCourses[i,j,m] = tempArray[k];   
                array3dStrings[i,j,m] = "" + array3dCourses[i,j,m].group + " " + array3dCourses[i,j,m].subject + " " + array3dCourses[i,j,m].teacher;
                m++;
            }
        }
    }
}

PrintExcel.Main(array3dStrings);

//Debug Check for 6period Days
for (int i =0;i<array3dCourses.GetLength(0);i++){
    Dictionary<string, int> teachersPerDay = new Dictionary<string, int>(); 
    for (int j = 0; j<array3dCourses.GetLength(1);j++){
        for (int k=0;k<array3dCourses.GetLength(2);k++){
            if (array3dCourses[i,j,k]!=null && teachersPerDay.ContainsKey(array3dCourses[i,j,k].teacher)){
                // If the key exists, add the value to the existing value
                teachersPerDay[array3dCourses[i,j,k].teacher]++;
                break;
            }
            else{
                // If the key does not exist, add the key with the value
                teachersPerDay.Add(array3dCourses[i,j,k].teacher, 1);
                break;
            }
        }
    }
    foreach (var pair in teachersPerDay){
        if (pair.Value > 5){
            Console.WriteLine(pair.Key + " teaches " + pair.Value + " periods on day " + (i+1));
        }
    }
}

