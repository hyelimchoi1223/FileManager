using System.Collections.Generic;
using System.IO;
using System.Web;

namespace FileDataManager.Function
{
    public class FileDownload
    {
        static Dictionary<string, string> fileExtension = new Dictionary<string, string>();

        private static void InitializeExtension()
        {
            fileExtension.Add(".doc", "application/msword");
            fileExtension.Add(".docx", "application/msword");
            fileExtension.Add(".xls", "application/vnd.ms-excel");
            fileExtension.Add(".xlsx", "application/vnd.ms-excel");
            fileExtension.Add(".ppt", "application/vnd.ms-powerpoint");
            fileExtension.Add(".pptx", "application/vnd.ms-powerpoint");
            fileExtension.Add(".zip", "application/zip");
            fileExtension.Add(".pdf", "application/pdf");
            fileExtension.Add(".txt", "application/plain");
        }

        public static string GetExtension(FileInfo fileInfo)
        {
            string sMIMETYPE = "application/octet-stream";

            if (fileInfo == null || !fileExtension.ContainsKey(fileInfo.Extension))
                return sMIMETYPE;

            sMIMETYPE = fileExtension[fileInfo.Extension];
            if (sMIMETYPE == "")
            {
                sMIMETYPE = "application/octet-stream";
            }

            return sMIMETYPE;
        }

        public static bool Download(HttpResponse response, string filePath)
        {
            InitializeExtension();

            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                // Clear Rsponse reference  
                response.Clear();
                response.ClearHeaders();
                response.ClearContent();
                // Add header by specifying file name  
                response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                // Add header for content length  
                response.AddHeader("Content-Length", file.Length.ToString());
                // Specify content type  
                response.ContentType = GetExtension(file);
                // Clearing flush  
                response.Flush();
                // Transimiting file  
                response.TransmitFile(file.FullName);
                response.End();
                return true;
            }
            return false;
        }
    }
}
