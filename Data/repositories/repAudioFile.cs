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
    public class repAudioFile:IrepAudioFile
    {

        private readonly DataContaxt _context;

        public repAudioFile(DataContaxt context)
        {
            _context = context;
        }
        public async Task<int> addAsync(UploadViewModel userAndFile)
        {
            // שמירת פרטי הקובץ במסד הנתונים
            var audioFile = new MusicFile
            {
                FileName = userAndFile.File.FileName,
                MimeType = userAndFile.File.ContentType,
                Size = userAndFile.File.Length,
                FilePath = userAndFile.File.FilePath,
                Cost=userAndFile.File.Cost,
                UserId= userAndFile.User.Id
            };
            userAndFile.User.Currency.sum += userAndFile.CostFile;
            userAndFile.User.Files.Add(audioFile);

            _context.AudioFiles.Add(audioFile);
            await _context.SaveChangesAsync();

            return audioFile.Id;
        }

    }
}
