using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using OfficeOpenXml;
using XMLNamespace;
using ParserNamespace;

namespace MainProject
{
    class Program
    {
        static void Main()
        {
            string FilePath = "./xmlfile.xml"; // Replace with a file request functionality
            List<string> filelist = new List<string> { FilePath }; // Example, change to multiple files as needed
            if (filelist != null) // Check if filelist is not null
            {
                foreach (string file in filelist)
                {
                    if (!File.Exists(file))
                    {
                        throw new ArgumentException($"File: {file} has not been found! Need at least 3 files to continue.");
                    }
                }
            }

            // Assuming this is how you get your parser and data
            IFileParser FileParser = FactoryParsers.GetParser(filelist);
            Dictionary<string, DataTable> dt = FileParser.ParseFileToDatatable(filelist);
            /*
            // Create the Excel file and write DataTable data
            string excelFilePath = "outputlong.xlsx"; // Path where Excel file will be saved

            using (var package = new ExcelPackage())
            {
                // Loop through each DataTable in the dictionary
                foreach (DataTable dattane in dt.Values)
                {
                    // Add a worksheet to the Excel package
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Load the DataTable into the worksheet starting from cell A1
                    worksheet.Cells["A1"].LoadFromDataTable(dattane, true); // 'true' for including headers

                    // Optionally, you can change the sheet name based on a specific key or data
                }

                // Save the Excel file to the given file path
                FileInfo fi = new FileInfo(excelFilePath);
                package.SaveAs(fi);

                Console.WriteLine("Excel file created successfully at: " + excelFilePath);
            }
            */
        }
    }
}
