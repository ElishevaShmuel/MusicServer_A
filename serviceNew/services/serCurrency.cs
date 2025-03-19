using Core.Entities;
using Core.Irepository;
using Core.Iservice;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.services
{
    public class serCurrency : IserCurrency
    {
        private readonly IrepCurrency _repository;
        private readonly IConfiguration _configuration;


        public serCurrency(IrepCurrency repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public async Task<int> getSumAsync(string userId)
        {
            return await _repository.getSumAsync(userId);
        }
        public async Task<int> AddAsync(int cost,string userId) {
            return await _repository.AddAsync(cost, userId);
        }
        public async Task<int> SubAsync(int cost, string userId) { 
            return await _repository.SubAsync(cost, userId);
        }
    }
}
