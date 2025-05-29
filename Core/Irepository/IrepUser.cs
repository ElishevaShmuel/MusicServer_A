using Core.Entities;
using Core.Iservice;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Irepository
{
    public interface IrepUser
    {
       
        public Task<int> Register(User user);
        public Task<User> Login(LoginDto user);
        public void Logout(User user) { }
        public Task<User> getProfile(int id);

        public Task<User?> ValidateAdminLogin(string email);
        public Task<List<User>> getAllUserAsync();
        public Task<int> DeleteUser(User u);

    }
}
