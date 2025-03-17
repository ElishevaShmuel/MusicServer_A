using Microsoft.Extensions.Configuration;
using Core.Irepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Core.Iservice;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Service.services
{
    class serAudioFile:IserAudioFile
    {
        private readonly IrepAudioFile _repository;
        private readonly IConfiguration _configuration;


        public serAudioFile(IrepAudioFile repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }        

        public async Task<FileContentResult> ReadAsync(string filePath,string fileName)
        {

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var mimeType = "audio/mpeg"; // או סוג התוכן המתאים לקובץ שלך
            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };
        }
    }
        public async Task<int> WriteAsync(IFormFile file)
        {
            var filePath = Path.Combine("path/path", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
           var id= await _repository.addAsync(file,filePath);

           return id;
        }
    }
}
