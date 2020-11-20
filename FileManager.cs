using ExcelDataReader;
using LinkGenesis.FileDataManager.EnumData;
using LinkGenesis.FileDataManager.Model;
using System.IO;

namespace LinkGenesis.FileDataManager
{

    public class FileManager
    {
        private string _filePath;
        private FileType _fileType;
        public FileManager(string path)
        {
            FileInfo fi = new FileInfo(path);
            _filePath = path;
            //_fileType = (FileType)Enum.Parse(typeof(FileType), fi.Extension.Replace(".", "").ToLower());
        }
        public ExcelInfo ImportFileLoad()
        {
            using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {                    
                    ExcelInfo info = new ExcelInfo(reader.AsDataSet());
                    return info;
                }
            }
        }
    }
}
