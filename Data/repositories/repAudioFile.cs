using Core.Entities;
using Core.Irepository;
using Data.data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.repositories
{
    class repAudioFile:IrepAudioFile
    {

        private readonly DataContaxt _context;

        public repAudioFile(DataContaxt context)
        {
            _context = context;
        }
        public async Task<int> addAsync(IFormFile file,string filePath)
        {
            // שמירת פרטי הקובץ במסד הנתונים
            var audioFile = new MusicFile
            {
                FileName = file.FileName,
                MimeType = file.ContentType,
                Size = file.Length,
                FilePath = filePath
            };

            _context.AudioFiles.Add(audioFile);
            await _context.SaveChangesAsync();

            return audioFile.Id;
        }

    }
}
