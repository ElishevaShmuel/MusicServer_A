using Core.Irepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User
    {
    
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        [Required]
        public string Password { get; set; }
        [StringLength(100)]
        public string PasswordHash { get; set; }
        [StringLength(100)]
        public string ProfilePicturePath { get; set; }
        [StringLength(100)]
        public string Role { get; set; }
        public List<MusicFile> Files { get; set; } = new List<MusicFile>();

        public Currency Currency { get; set; } = new Currency();


        public User() { }

        public User(int id, string name, string email, string password, string profilePicturePath, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            ProfilePicturePath = profilePicturePath;
            Role = role;
        }
        public User(int id, string name, string email, string password, string role)
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
    [Required]
    public string name { get; set; }  // שינה מ-Username ל-Name
    
    [Required]
    [EmailAddress]
    public string email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string password { get; set; }
    
    
    [Required]
    public string role { get; set; }
}
    public class LoginDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }

}
