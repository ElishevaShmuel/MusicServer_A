using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Iservice
{
    public interface IserUser
    {
        public Task<int> Register(RegisterDto registerDto);
        public Task<User> Login(LoginDto user);
        public void Logout(User user) { }

        public Task<User> getProfile(int id);
    }
}
