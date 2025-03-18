using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Iservice
{
    public interface IserCurrency
    {
        public Task<int> getSumAsync(string userId);
        public Task<int> AddAsync(int cost, string userId);
        public Task<int> SubAsync(int cost, string userId);
    
}
}
