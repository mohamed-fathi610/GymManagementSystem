using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Helpers
{
    public interface IAttachementService
    {
        string? Upload(string folderName, IFormFile file);

        bool Delete(string fileName, string folderName);
    }
}
