using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitch_Scheduling_Machine
{
    public class MikeMakeSchedule
    {
            public static void makeSchedule(){
            

            string[,] matrix = {
                { "A1", "B2", "C3", "D4", "E5" },
                { "F6", "G7", "H8", "I9", "J11" },
                // { "K11", "L12", "M13", "N14", "O15" },
                // { "P16", "Q17", "R18", "S19", "T20" },
                // { "U21", "V22", "W23", "X24", "Y25" },
                // { "Z26", "AA27", "AB28", "AC29", "AD30" }
            };

            // Flatten the matrix to a list
            List<string> list = FlattenMatrix(matrix);

            // Generate all permutations of the list
            List<List<string>> permutations = GetPermutations(list, list.Count);

            // Print each permutation as a matrix
            int permCount = 1; // To keep track of permutation number
            foreach (var perm in permutations)
            {
                string[,] permMatrix = ReconstructMatrix(perm, matrix.GetLength(0), matrix.GetLength(1));
                Console.WriteLine($"Permutation {permCount++}:");
                PrintMatrix(permMatrix);
                Console.WriteLine();
            }
        }

        static List<string> FlattenMatrix(string[,] matrix)
        {
            List<string> list = new List<string>();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    list.Add(matrix[i, j]);
                }
            }

            return list;
        }

        static List<List<T>> GetPermutations<T>(List<T> list, int length)
        {
            List<List<T>> result = new List<List<T>>();
            Permute(list, 0, length - 1, result);
            return result;
        }

        static void Permute<T>(List<T> list, int l, int r, List<List<T>> result)
        {
            if (l == r)
            {
                result.Add(new List<T>(list));
            }
            else
            {
                for (int i = l; i <= r; i++)
                {
                    Swap(list, l, i);
                    Permute(list, l + 1, r, result);
                    Swap(list, l, i); // backtrack
                }
            }
        }

        static void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        static string[,] ReconstructMatrix(List<string> list, int rows, int cols)
        {
            string[,] matrix = new string[rows, cols];
            int index = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = list[index++];
                }
            }

            return matrix;
        }

        static void PrintMatrix(string[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Determine the maximum length of the strings in the matrix
            int maxLength = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j].Length > maxLength)
                    {
                        maxLength = matrix[i, j].Length;
                    }
                }
            }

            // Print each row of the matrix
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Use String.Format for dynamic width formatting
                    Console.Write(string.Format("{0,-" + maxLength + "} ", matrix[i, j]));
                }
                Console.WriteLine();
            }
        }
    }
}