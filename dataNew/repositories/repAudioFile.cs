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
        public async Task<List<MusicFile>> getAllFiles()
        {
            return await _context.AudioFiles.ToListAsync();
        }
        public async Task<MusicFile> getById(int id)
        {
            return _context.AudioFiles.FirstOrDefault(f => f.UserId == id);
        }

        public async Task<int> addAsync(MusicFileDto file,string filePath)
        {

            var user = _context.Users
                .Include(u => u.Currency)
                .FirstOrDefault(u => u.Id == file.UserId);
            if (user == null)
                return -1;
            // שמירת פרטי הקובץ במסד הנתונים
            var audioFile = new MusicFile
            {
                FileName = file.FileName,
                MimeType = file.MimeType,
                Size = file.Size,
                FilePath = file.FilePath,
                Cost = file.Cost,
                UserId = file.UserId,
                User = user
            };
            user.Currency.sum += file.Cost;
            //user.Files.Add(audioFile);

            _context.AudioFiles.Add(audioFile);
            await _context.SaveChangesAsync();
            

            return audioFile.Id;
        }
        public async Task<int> removeAsync(MusicFileDto file)
        {
            // מחיקת הקובץ ממסד הנתונים
            var audioFile = await _context.AudioFiles.FindAsync(file.Id);
            if (audioFile == null)
            {
                return -1; // קובץ לא נמצא
            }
            _context.AudioFiles.Remove(audioFile);
            var user = _context.Users.FirstOrDefault(u => u.Id == file.UserId);
            //user.Files.Remove(audioFile);
            await _context.SaveChangesAsync();
            return 1; // הצלחה
        }

    }
}
