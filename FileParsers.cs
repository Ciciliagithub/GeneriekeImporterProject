using System;
using System.Data;
using System.IO;
using System.Xml;
//Usable namespaces
using XMLNamespace;

namespace ParserNamespace
{
    interface IFileParser
    {
        Dictionary<string,DataTable> ParseFileToDatatable(List<string> path);//Converts file to datatable & [reads it for user -> nodig? ] 
        DataTable FileProcessor(DataTable dt);
    }

    public class XMLParser : IFileParser
    {
        public Dictionary<string, DataTable> ParseFileToDatatable(List<string> FilePaths)
        {
            Dictionary<string, DataTable> DatatableDictionary = new Dictionary<string, DataTable>();

            Parallel.ForEach(FilePaths, paths =>
            {
                DataTable dt = XmlBundleClass.ConvertXmlToDataTable(paths);//XmlBundleClass.IterateOverDt(dt); 
                DatatableDictionary[paths] = dt;
                Console.WriteLine($"File: {paths} has been found and converted into Datatable: {dt.TableName}!\nWith elements:");
            });
           // XmlBundleClass.IterateOverDt(DatatableDictionary);
            return DatatableDictionary;
           // XmlBundleClass.IterateOverDt(dt);
            //XmlBundleClass.IterateOverDt(dt);//XmlBundleClass.IterateOverDt(dt);
             // dt = FileProcessor(dt);
            //XmlBundleClass.GetAllColumns(dt);//Hier pak je al de columns en kan je aan user laten zien wat ze zijn
            //XmlBundleClass.GetColumnValues(dt,"Body_GetResultResponse_GetResultResult_Report_ReportPart_eis_eistekst");//Hier pak je al de columns en kan je aan user laten zien wat ze zijn
            //XmlBundleClass.GetThirdColumnValues(dt);
            Console.WriteLine("Datatable has been found and saved!");
            //return dt;

        }

        public DataTable FileProcessor(DataTable dt)
        {
            throw new NotImplementedException();//Hier proces ik de xml veranderde datatable op fouten zoals Parent.Child.Child -> Child 
        }
    }
    
    public class ExcelParser : IFileParser
    {
        public Dictionary<string, DataTable> ParseFileToDatatable(List<string> path)//Hier moet dan de parser komen
        {
            throw new NotImplementedException();
        }

        public DataTable FileProcessor(DataTable dt)
        {
            throw new NotImplementedException();//Hier proces ik de xml veranderde datatable op fouten zoals Parent.Child.Child -> Child 
        }
    }


    public class JsonParser : IFileParser
    {
        public Dictionary<string, DataTable> ParseFileToDatatable(List<string> path)//Hier moet dan de parser komen
        {
            throw new NotImplementedException();
        }
        public DataTable FileProcessor(DataTable dt)
        {
            throw new NotImplementedException();//Hier proces ik de xml veranderde datatable op fouten zoals Parent.Child.Child -> Child 
        }
    }


    class FactoryParsers
    {
        public static IFileParser GetParser(List<string> FileTypes)//Logica wat verbeteren om combined imports te supporten
        {//Path.GetExtension(filelist[0])
            foreach (string file in FileTypes)
            {
                switch(Path.GetExtension(file).ToLower())
                {
                    case ".xml":
                        return new XMLParser();

                    case ".xlsx":
                        return new JsonParser();

                    case ".json":
                        return new ExcelParser();
                    //Any other filetype is addable, as long as the interface along with it's implementation is added.
                    default:
                        Console.WriteLine($"Error! File: {file} has unsupported extension {Path.GetExtension(file)}");
                        break;                    
                }
            }
            throw new ArgumentException();
           // Console.WriteLine($"File has been detected as an {FileType} file!\n");
        }
    }

}
