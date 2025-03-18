using Core.Irepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePicturePath { get; set; }
        public string Role { get; set; }
        public List<MusicFile> Files { get; set; }
        public Currency Currency { get; set; }


        public User() { }

        public User(string id, string name, string email, string password, string profilePicturePath, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            ProfilePicturePath = profilePicturePath;
            Role = role;
        }
        public User(string id, string name, string email, string password, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            ProfilePicturePath = "/uploads/default.jpg";
            Role = role;
        }
    }
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
