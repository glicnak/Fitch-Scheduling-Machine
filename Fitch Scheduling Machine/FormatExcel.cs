using Microsoft.VisualBasic;

namespace Fitch_Scheduling_Machine;
using System;
using System.Drawing;
using System.IO;
// using DocumentFormat.OpenXml.Drawing.Charts;
using OfficeOpenXml;
using OfficeOpenXml.Style;


    public class FormatExcel
    {
        
        public static void Main(string[,] transposedArray, int x){

            string filePathcsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..","..","ScheduleA2D.csv");
            string xlsxFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..","..","ScheduleA.xlsx");

            // Read the CSV file
            var csvLines = File.ReadAllLines(filePathcsv);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < csvLines.Length; i++)
                {
                    var columns = csvLines[i].Split(',');

                    for (int j = 0; j < columns.Length; j++)
                    {
                        worksheet.Cells[i + 1, j + 1].Value = columns[j];
                    }
                }

                // Save the Excel file
                FileInfo fileInfo2 = new FileInfo(xlsxFilePath);
                package.SaveAs(fileInfo2);

                // var fileInfo2 = new FileInfo(xlsxFilePath);
                // using (var package2 = new ExcelPackage(fileInfo2));



            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..","..","..","ScheduleA.xlsx");

            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file does not exist.");
                return;
            }

            // Open the existing Excel package
            FileInfo fileInfo = new FileInfo(filePath);

            using (var package = new ExcelPackage(fileInfo))
            {
                // Get the first worksheet (or specify by name/index)
                var worksheet = package.Workbook.Worksheets[0];

                int[] numbers = { 1, 2, x+3, x*2+4, x*3+5, x*4+6, x*5+7};

                foreach (int number in numbers)
                {

                    // Define row numbers where blank rows are to be inserted
                    int rowToInsert =  number;

                    // Shift rows down to make space for the new row
                    worksheet.InsertRow(rowToInsert, 1);

                }

                // Insert two blank columns on the left for titles
                int[] columnsToInsert = { 1, 2 };

                // Insert columns from left to right
                Array.Sort(columnsToInsert);
                foreach (int columnToInsert in columnsToInsert)
                {
                    if (columnToInsert >= 1 && columnToInsert <= worksheet.Dimension.End.Column)
                    {
                        worksheet.InsertColumn(columnToInsert, 1);
                    }
                }

                // Define ranges to be merged (title ranges - header, P1, P2, etc)
                List<string> cellmergeRanges = new List<string>
                {
                    "C1:G1", // Range from C1 to G1 (DAY/header)
                    $"A3:A{x + 2}",  // Range from A3 to end of P1
                    $"A{x + 4}:A{2 * x + 3}",  // Range for P2
                    $"A{2 * x + 5}:A{3 * x + 4}",  // Range for P3
                    $"A{3 * x + 6}:A{4 * x + 5}",  // Range for P4
                    $"A{4 * x + 7}:A{5 * x + 6}",  // Range for P5
                    $"A{5 * x + 8}:A{6 * x + 7}",  // Range for P6
                };

                // Create list of only Period title ranges (P1, P2, etc)
                List<string> periodtitleRanges = cellmergeRanges.Except(new List<string> { "C1:G1" }).ToList();

                // Loop through cellmergeRanges to merge
                foreach (var mergerange in cellmergeRanges)
                {
                    var cells = worksheet.Cells[mergerange];
                    cells.Merge = true;
                }

                // Loop through periodtitleRanges (P1-P6 titles) to format cell colour, size, border 
                foreach (var formatrange in periodtitleRanges)
                {
                    // cell colour, size, border for P1-P6 cells
                    worksheet.Cells[$"{formatrange}"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                    worksheet.Cells[$"{formatrange}"].Style.Font.Size = 25;
                    worksheet.Cells[$"{formatrange}"].Style.Border.BorderAround(ExcelBorderStyle.Thick, System.Drawing.Color.Black);
                    worksheet.Cells[$"{formatrange}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[$"{formatrange}"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                // Initialize dictionary for titles
                Dictionary<string, string> titleDictionary = new Dictionary<string, string>
                {
                    { "C1", "DAY"}, { "C2",  "Monday"}, { "D2",  "Tuesday"}, { "E2",  "Wednesday"},
                    { "F2",  "Thursday"}, { "G2",  "Friday"}, { "A3",  "P1"}, { $"A{x+4}", "P2"},
                    {$"A{2*x+5}", "P3"}, {$"A{3*x+6}", "P4"}, {$"A{4*x+7}", "P5"}, {$"A{5*x+8}", "P6"}
                    
                };
                
                // Format range of cells (titles)
                var range = worksheet.Cells["C1:G2"];
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 25;
                range.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                range.Style.Border.BorderAround(ExcelBorderStyle.Thick, System.Drawing.Color.Black);
                range.AutoFitColumns();

                // Set cell values
                foreach (var entry in titleDictionary)
                {
                    worksheet.Cells[entry.Key].Value = entry.Value;
                };

                // Define a list to hold cell ranges to be blacked out
                List<string> cellblackoutRanges =
                [
                    // Define row ranges to be blacked out between periods
                    $"C{x+3}:G{x+3}", $"C{2*x+4}:G{2*x+4}", $"C{3*x+5}:G{3*x+5}", $"C{4*x+6}:G{4*x+6}", $"C{5*x+7}:G{5*x+7}", 
                ];

                foreach(var blackoutRange in cellblackoutRanges)
                {
                    worksheet.Cells[$"{blackoutRange}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[$"{blackoutRange}"].Style.Fill.BackgroundColor.SetColor(Color.Black);
                };

                // Define the range of cells for borders
                var borderRange = worksheet.Cells[$"C1:G{6*x+7}"]; // Example range A1 to D10

                // Set border outline for each cell in the range
                var border = borderRange.Style.Border;
                border.Top.Style = border.Bottom.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                border.Top.Color.SetColor(System.Drawing.Color.Black);
                border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                border.Left.Color.SetColor(System.Drawing.Color.Black);
                border.Right.Color.SetColor(System.Drawing.Color.Black);

                // Set column width for columns 1 and 2
                worksheet.Column(1).Width = 8.43; // Set width of column A
                worksheet.Column(2).Width = 3.14; // Set width of column B

                List<int> dayColumnslist = new List<int> { 3, 4, 5, 6, 7 };

                foreach(int number in dayColumnslist)
                {
                    worksheet.Column(number).Width = 30; // Set width of column
                };

                // Set row height for top two rows
                worksheet.Row(1).Height = 50; // Set height of row 1
                worksheet.Row(2).Height = 35; // Set height of row 2

                // Save the changes to the file
                package.Save();
            }


        }
        
    }