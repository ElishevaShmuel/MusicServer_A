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
        public Task<int> getSumAsync(string userId);
        public Task<int> AddAsync() { }
        public Task<int> SubAsync() { }
    }
}
