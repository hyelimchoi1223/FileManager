using ExcelDataReader;
using LinkGenesis.FileDataManager.EnumData;
using LinkGenesis.FileDataManager.Model;
using System;
using System.IO;

namespace LinkGenesis.FileDataManager.Function
{
    /// <summary>
    /// ExcelDataReader Descripting Url : 
    /// https://github.com/ExcelDataReader/ExcelDataReader
    /// </summary>
    public class FileImport
    {
        public static ExcelInfo Import(string filePath)
        {
            FileType fileType = (FileType)Enum.Parse(typeof(FileType), Path.GetExtension(filePath).Substring(1).ToLower());
            ExcelInfo result = null;
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                switch (fileType)
                {
                    case FileType.xls:
                    case FileType.xlsx:
                        result = XlsxReader(stream);
                        break;
                    case FileType.csv:
                    case FileType.txt:
                        result = CsvReader(stream);
                        break;
                    default:
                        break;
                }
                return result;
            }
        }

        private static ExcelInfo CsvReader(FileStream stream)
        {
            using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
            {
                ExcelInfo info = new ExcelInfo(reader.AsDataSet());
                return info;
            }
        }

        private static ExcelInfo XlsxReader(FileStream stream)
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                ExcelInfo info = new ExcelInfo(reader.AsDataSet());
                return info;
            }
        }
    }
}
