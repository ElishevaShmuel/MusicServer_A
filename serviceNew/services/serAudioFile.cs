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
        public async Task<List<MusicFile>> getAllFiles()
        {
           return await _repository.getAllFiles();
        }
        public async Task<MusicFile> getById(int id)
        {
            return await _repository.getById(id);
        }

        public async Task<int> removeAsync(MusicFileDto file)
        {
            return await _repository.removeAsync(file);

        }


        public async Task<int> WriteAsync(MusicFileDto file)
        {
            var id = await _repository.addAsync(file,file.FilePath);

            return id;
        }
    }
}

