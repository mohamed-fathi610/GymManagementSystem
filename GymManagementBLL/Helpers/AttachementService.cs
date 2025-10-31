using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Helpers
{
    public class AttachementService : IAttachementService
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly IWebHostEnvironment _webHost;
        private const long maxFileSize = 5 * 1024 * 1024; // 5 MB

        public AttachementService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName == null || file == null || file.Length == 0)
                {
                    return null;
                }
                if (file.Length > maxFileSize)
                {
                    return null;
                }

                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    return null;
                }

                var folderPath = Path.Combine(_webHost.WebRootPath, "Images", folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid().ToString() + extension;

                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Upload Photo in {folderName}: bec:{ex}");
                return null;
            }
        }

        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName))
                {
                    return false;
                }

                var fulePath = Path.Combine(_webHost.WebRootPath, "Images", folderName, fileName);

                if (File.Exists(fulePath))
                {
                    File.Delete(fulePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file :{ex}");
                return false;
            }
        }
    }
}
