using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Iservice
{
    public interface IserAudioFile
    {
        public Task<int> WriteAsync(IFormFile file);

        public Task<FileContentResult> ReadAsync(string filePath,string fileName);

    }
}
