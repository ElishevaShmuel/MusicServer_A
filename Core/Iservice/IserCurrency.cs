using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Iservice
{
    public interface IserCurrency
    {
        public Task<int> getSumAsync(string user);
        public Task<int> AddAsync() { }
        public Task<int> SubAsync() { }
    
}
}
