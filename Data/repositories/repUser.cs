using Core.Entities;
using Core.Irepository;
using Data.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.repositories
{
    public class RepUser : DataContaxt,IrepUser
    {
        private readonly DataContaxt _context;

        public RepUser(DataContaxt context)
        {
            _context = context;
        }

        public async Task<int> Register(User user)
        {

            if (_context.Users.Any(u => u.Email == user.Email))
                return -1;

            _context.Users.Add(user);
            _context.Currencies.Add(user.Currensy);
            await _context.SaveChangesAsync();
            return 200;
        }



        public async Task<User> Login(LoginDto dto)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        }
        public void Logout(User user) { }

        public async Task<User> getProfile(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
