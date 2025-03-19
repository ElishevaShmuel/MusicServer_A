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
    public class serAudioFile : IserAudioFile
    {
        private readonly IrepAudioFile _repository;
        private readonly IConfiguration _configuration;


        public serAudioFile(IrepAudioFile repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<FileContentResult> ReadAsync(UploadViewModel userAndFileCost, string filePath)
        {
            userAndFileCost.User.Currency.sum -= userAndFileCost.CostFile;

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var mimeType = "audio/mpeg";
            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = filePath.Split('/')[filePath.Split('/').Length-1],
            };
        }

        public async Task<int> WriteAsync(UploadViewModel userAndFile)
        {
            var filePath = Path.Combine("path/path", userAndFile.File.FileName);
            userAndFile.User.Currency.sum -= userAndFile.CostFile;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await userAndFile.File.CopyToAsync(stream);
            }
            var id = await _repository.addAsync(userAndFile,filePath);

            return id;
        }
    }
}

