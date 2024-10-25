using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Hospital.Utilities
{
    public class ImageOperations
    {
        private readonly IWebHostEnvironment _env;

        public ImageOperations(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string ImageUpload(IFormFile file)
        {
            string fileName = null;
            // Null Check to make sure that the method won't upload empty or non Exist file.
            if (file != null)
            {
                string fileDirectory = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                // To Generate a unique name for the uploaded file.
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                // combines the file directory and filename to create the full path.
                string filePath = Path.Combine(fileDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
