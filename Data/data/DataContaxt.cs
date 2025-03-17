using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.data
{
    public class DataContaxt : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<MusicFile> AudioFiles { get; set; }
        public DbSet<Currency> Currencies { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Music");
        }
    }
}
