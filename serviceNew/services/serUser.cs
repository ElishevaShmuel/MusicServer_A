using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Irepository;
using Core.Iservice;

namespace Service.services
{
    public class serUser : IserUser
    {
        private readonly IrepUser _repository;
        private readonly IConfiguration _configuration;

        public serUser(IrepUser repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public Task<int> Register(RegisterDto dto)
        {
            var passwordHash = dto.password.Length%3+5.ToString();// הצפנת הסיסמה

            var user = new User
            {
                Name = dto.name,
                Email = dto.email,
                Password = dto.password,
                PasswordHash = passwordHash.ToString(),
                Currency = new Currency(),
                ProfilePicturePath = "/uploads/default.jpg",
                Role = dto.role,
                Files = new List<MusicFile>()
            };
            user.Currency.userId = user.Id;
            user.Currency.sum = 0;


            return _repository.Register(user);
        }


        public async Task<User> Login(LoginDto dto)
        {
            var user = await _repository.Login(dto);
            var passwordHash = dto.password.Length % 3 + 5.ToString();
            if (user == null || passwordHash!= user.PasswordHash)
            {
                return null;
            }
            return user;
        }
        public void Logout(User user) { }


        public async Task<User> getProfile(int id)
        {
            return await _repository.getProfile(id);
        }

        public async Task<User> ValidateAdminLogin(string email,string password)
        {
            var adminUser = await _repository.ValidateAdminLogin(email);

            if (adminUser == null)
                return null;

            // בדיקת סיסמה - התאם לשיטת ההצפנה שלך
            if (VerifyPassword(password))
            {
                return adminUser;
            }

            return null;
        }

        public async Task<int> DeleteUser(User u)
        {
            if (u.Email == null)
                return -1;
           return await _repository.DeleteUser(u);
        }


        public async Task<List<User>> getAllUserAsync()
        {
            return await _repository.getAllUserAsync();
        }


        //// או אלטרנטיבה עם רשימה קבועה:
        //public async Task<User?> ValidateAdminLoginWithPredefinedList(string email, string password)
        //{
        //    // רשימת מנהלים מוגדרת מראש
        //    var predefinedAdmins;

        //    var adminCredentials = predefinedAdmins
        //        .FirstOrDefault(a => a.Email == email && a.Password == password);

        //    if (adminCredentials.Email != null)
        //    {
        //        // יצירת אובייקט User זמני עבור המנהל
        //        return new User
        //        {
        //            Id = 0, // או ID מיוחד למנהלים
        //            Email = adminCredentials.Email,
        //            Name = adminCredentials.Name,
        //            Role = "Admin"
        //        };
        //    }

        //    return null;
        //}

        private bool VerifyPassword(string password)
        {
            
            return password == "admin123"; 
        }

    }


}

