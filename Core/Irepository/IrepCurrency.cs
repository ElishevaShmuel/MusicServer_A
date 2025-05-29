using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Irepository
{
    public interface IrepCurrency
    {
        public Task<int> getSumAsync(int userId);
        public Task<int> AddAsync(int cost,int usrId);
        public Task<int> SubAsync(int cost,int userId);
    }
}
