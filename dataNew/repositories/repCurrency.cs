using Core.Entities;
using Core.Irepository;
using Data.data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.repositories
{
    public class repCurrency : IrepCurrency
    {

        private readonly DataContaxt _context;

        public repCurrency(DataContaxt context)
        {
            _context = context;
        }
        public async Task<int> getSumAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return -1;
            var sum = _context.Currencies.Where(c => c.userId == user.Id).Sum(c => c.sum);
            return sum;
        }
        public async Task<int> AddAsync(int cost, int userId)
        {var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return -1;
           
            
            user.Currency.sum += cost;
            await _context.SaveChangesAsync();
            return user.Currency.sum;
        }
        public async Task<int> SubAsync(int cost, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return -1;
            user.Currency.sum -= cost;
            await _context.SaveChangesAsync();
            return user.Currency.sum;
        }
    }
}
