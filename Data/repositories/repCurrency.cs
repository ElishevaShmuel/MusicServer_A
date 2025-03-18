using Core.Entities;
using Core.Irepository;
using Data.data;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<ApplicationUser> _userManager;
        public repCurrency(DataContaxt context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<int> getSumAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return -1;
            var sum = _context.Currencies.Where(c => c.userId == user.Id).Sum(c => c.sum);
            return sum;
        }
        public async Task<int> AddAsync(int cost, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return -1;
            await _context.Users.FindAsync(user.Id).Currensy.sum += cost;
            await _context.SaveChangesAsync();
            return user.Currency.sum;
        }
        public async Task<int> SubAsync(int cost, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return -1;
            await _context.Users.FindAsync(user.Id).Currensy.sum -= cost;
            await _context.SaveChangesAsync();
            return user.Currency.sum;
        }
    }
}
