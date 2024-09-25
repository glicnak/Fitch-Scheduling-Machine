namespace Fitch_Scheduling_Machine;
using System;
using System.IO;
using System.Text;

class PrintExcel
{
    public static void Main(string[,,] schedule3dArrayStrings)
    {
        // Print the entire 3D array
        //Print3DArray(schedule3dArrayStrings);

        int x = schedule3dArrayStrings.GetLength(2);

        // write the array to a csv file
        Write3DArrayToCsv(schedule3dArrayStrings, "ScheduleA3D.csv");

        // Convert the 3D array to a 2D array by flattening each layer into a single row
        string[,] array2D = Flatten3DLayerTo2D(schedule3dArrayStrings);

        // Print the 2D array
        //Print2DArray(array2D);

        // Transpose the 2D array
        string[,] transposedArray = Transpose2DArray(array2D);

        // Write the 2D array to a CSV file
        Write2DArrayToCsv(transposedArray, "scheduleA2D.csv");

        //convert to xlsx & format
        FormatExcel.Main(transposedArray, x);

    }

    static void Print3DArray(string[,,] array3d)
    {
        // layers = days, columns = periods, rows = groups/classes
        int layers = array3d.GetLength(0);
        int rows = array3d.GetLength(1);
        int cols = array3d.GetLength(2);

        for (int i = 0; i < layers; i++)
        {
            Console.WriteLine($"Layer {i}:");

            for (int j = 0; j < rows; j++)
            {
                for (int k = 0; k < cols; k++)
                {
                    Console.Write(array3d[i, j, k] + "\t");
                }
                Console.WriteLine(); // New line for the next row
            }
            Console.WriteLine(); // New line for the next layer
        }
    }


    static string[,] Flatten3DLayerTo2D(string[,,] array3D)
        {
            int layers = array3D.GetLength(0);
            int rows = array3D.GetLength(1);
            int cols = array3D.GetLength(2);

            // New 2D array will have 'layers' rows and 'rows * cols' columns
            string[,] array2D = new string[layers, rows * cols];

            for (int k = 0; k < layers; k++)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        // Flatten the 2D layer into a single row of the 2D array
                        int index = i * cols + j;
                        array2D[k, index] = array3D[k, i, j];
                    }
                }
            }

            return array2D;
        }

        static void Print2DArray(string[,] array2D)
        {
            int rows = array2D.GetLength(0);
            int cols = array2D.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(array2D[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

    static string[,] Transpose2DArray(string[,] array2D)
        {
            int rows = array2D.GetLength(0);
            int cols = array2D.GetLength(1);

            // Create a new array with transposed dimensions
            string[,] transposed = new string[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    transposed[j, i] = array2D[i, j];
                }
            }

            return transposed;
        }

    static void Write3DArrayToCsv(string[,,] array3d, string filePath)
    {
        int layers = array3d.GetLength(0);
        int rows = array3d.GetLength(1);
        int cols = array3d.GetLength(2);

        var sb = new StringBuilder();

        for (int k = 0; k < layers; k++)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(array3d[k, i, j]);

                    if (j < cols - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }
            sb.AppendLine(); // Separate layers with a blank line
        }

        // Write the content to the CSV file
        string filePathcsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..","..","ScheduleA3D.csv");
        File.WriteAllText(filePathcsv, sb.ToString());
    }

        static void Write2DArrayToCsv(string[,] array, string filePath)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            var sb = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(array[i, j]);

                    if (j < cols - 1)
                        sb.Append(",");
                }
                sb.AppendLine();
            }

            // Write the content to the CSV file
            string filePathcsv2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..","..","ScheduleA2D.csv");
            File.WriteAllText(filePathcsv2, sb.ToString());

        }

    }