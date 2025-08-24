using Microsoft.AspNetCore.Http;
using n_Tier_Architecture.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n_Tier_Architecture.BLL.Services.Classes
{
    public  class FileService : IFileService
    {
        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
              var fileName =  Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
               //adding uploaded file to image folder 
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",fileName);
                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            throw new Exception("error ");
        }
    }
}
