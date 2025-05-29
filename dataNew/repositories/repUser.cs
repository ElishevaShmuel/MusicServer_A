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
    public class RepUser : IrepUser
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
            _context.Currencies.Add(user.Currency);
            await _context.SaveChangesAsync();
            return 200;
        }
        public async Task<int> DeleteUser(User u)
        {
            _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            return 1;
        }



        public async Task<User> Login(LoginDto dto)
        {
            var user = await _context.Users
                                .Include(u => u.Files)
                                .FirstOrDefaultAsync(u => u.Email == dto.email);
            return user;
        }

        public async Task<User> ValidateAdminLogin(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Role == "Admin");

        }
        public void Logout(User user) { }

        public async Task<User> getProfile(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<List<User>> getAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
