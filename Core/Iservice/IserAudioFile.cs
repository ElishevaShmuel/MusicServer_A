using Core.Entities;
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
        public Task<int> WriteAsync(UploadViewModel userAndFile);

        public Task<FileContentResult> ReadAsync(UploadViewModel userAndFileCost,string filePath);
        public Task<List<MusicFile>> getAllFiles();

    }
}
