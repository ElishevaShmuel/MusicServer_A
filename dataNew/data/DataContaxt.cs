//using AutoMapper.Configuration;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Data.data
{
    public class DataContaxt : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<MusicFile> AudioFiles { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        private readonly IConfiguration _configuration;

        public DataContaxt(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRINGS__DEFAULT_CONNECTION");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
   
    }
}
