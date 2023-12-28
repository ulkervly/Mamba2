using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiz.Business.Extensions.Helper
{
    public class Helper
    {
        public static string SaveFile(string rootPath, string folderName, IFormFile file)
        {
            string fileName = file.FileName.Length > 64 ? file.FileName.Substring(file.FileName.Length - 64, 64) : file.FileName;
            fileName = Guid.NewGuid().ToString() + fileName;

            string path = Path.Combine(rootPath, folderName, fileName);
            using (FileStream streem = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(streem);
            }
            return fileName;
        }
    }
}
