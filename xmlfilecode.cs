using System;
using System.Data;
using System.IO;
using System.Xml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;


namespace XMLNamespace
{
    public class XmlBundleClass {

        public static DataTable ConvertXmlToDataTable(string filePath)
        {
            DataTable dataTable = new DataTable();
            XDocument xDoc = XDocument.Load(filePath);
            XElement root = xDoc.Root;

            if (root == null || !root.Elements().Any())
                throw new Exception("Invalid XML structure: No root elements found.");

            // Flatten XML to handle dynamic structure
            ProcessDynamicXml(root, dataTable, "");
            GetAllColumns(dataTable);

            return dataTable;
        }

        private static void FlattenXmlToDataTable(XElement element, DataTable dataTable, string parentElementName)
        {
            foreach (var childElement in element.Elements())
            {
                string columnName = parentElementName + childElement.Name.LocalName;

                // Add column if it doesn't already exist
                if (!dataTable.Columns.Contains(columnName))
                    dataTable.Columns.Add(columnName, typeof(string));

                // Check if the child element has further nested elements (to recursively flatten them)
                if (childElement.HasElements)
                {
                    ProcessDynamicXml(childElement, dataTable, columnName + "_");
                }
                else
                {
                    // Add the value to the DataTable
                    DataRow row = dataTable.NewRow();
                    row[columnName] = childElement.Value.Trim();
                    dataTable.Rows.Add(row);
                }
            }
        }
    public static void IterateOverDt(Dictionary<string, DataTable> dt)
        {
            Console.WriteLine("\nDatable: " + dt );
            foreach (var datatable in dt)
            {
                var datatableiteration = datatable.Value;
                foreach (DataRow row in datatableiteration.Rows)
                {
                    foreach (DataColumn column in datatableiteration.Columns)
                    {
                        Console.Write(column.ColumnName + " : " + row[column] + " | ");//+ column.DataType
                    }
                    Console.WriteLine();
                }
            }
            return;
        }

    public static void GetAllColumns(DataTable dataTable)
    {
        // Ensure that the DataTable has at least 4 rows
        if (dataTable.Rows.Count >= 4)
        {
            // Get the fourth row (index 3, as row indices are zero-based)
            DataRow fourthRow = dataTable.Rows[3];

            // Iterate over all columns in the fourth row and print the values
            foreach (DataColumn column in dataTable.Columns)
            {
                var value = fourthRow[column];
                Console.WriteLine($"{column.ColumnName}: {value}");
            }
        }
        else
        {
            Console.WriteLine($"The DataTable: {dataTable} does not have a fourth row!");
        }
    }

    public static void GetColumnValues(DataTable dataTable, string columnName)
    {
        if (dataTable.Columns.Contains(columnName))
        {
            foreach (DataRow row in dataTable.Rows)
            {
                var value = row[columnName];
                Console.WriteLine($"{columnName} ++: {value}");
            }
        }
        else
        {
            Console.WriteLine($"The DataTable: {dataTable} does not contain a column named '{columnName}'.");
        }
    }
    
    public static void ProcessDynamicXml(XElement element, DataTable dataTable, string parentElementName)//Handmatig verbeteren volgende week
    {
        DataRow row = null;

        foreach (var childElement in element.Elements())
        {
            string columnName = parentElementName + childElement.Name.LocalName;

            if (!dataTable.Columns.Contains(columnName))
                dataTable.Columns.Add(columnName, typeof(string));

            if (row == null)
            {
                row = dataTable.NewRow();
            }

            if (childElement.HasElements)
            {
                ProcessDynamicXml(childElement, dataTable, columnName + "_");
            }
            else
            {
                row[columnName] = childElement.Value.Trim();
            }
        }

        if (row != null)
        {
            dataTable.Rows.Add(row);
        }
    }

    }
}
