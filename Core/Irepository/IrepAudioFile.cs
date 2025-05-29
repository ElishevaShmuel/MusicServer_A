using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Core.Irepository
{
    public interface IrepAudioFile
    {
        public Task<int> addAsync(MusicFileDto userAndFile,string filePath);
        public Task<List<MusicFile>> getAllFiles();
        public Task<MusicFile> getById(int id);
        public Task<int> removeAsync(MusicFileDto file);

    }
}
