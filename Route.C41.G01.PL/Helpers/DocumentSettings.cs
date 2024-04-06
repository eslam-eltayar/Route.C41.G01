using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Route.C41.G01.PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName)
        {
            /// 1. Get Located Folder Path

            //string folderPath = $"D:\\C#VS\\MvcProject\\Route.C41.G01.PL\\wwwroot\\files\\{folderName}";
            //string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folderName}";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            /// 2. Get File Name and Make it UNIQE

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            /// 3. Get File Path 

            string filePath = Path.Combine(folderPath, fileName);

            /// 4. Save File as Streams [Data per Time]

            using var fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return fileName;
            
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if(File.Exists(filePath))
                File.Delete(filePath);

        }
    }
}
