using Microsoft.Extensions.Configuration;
using Core.Entities;
using Core.Irepository;
//using Core.Iservice;
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
            var passwordHash = dto.Password.Length%3+5.ToString();// הצפנת הסיסמה
            
            var user = new User
            {
                Name = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash.ToString(),
                Currency = new Currency(),
                ProfilePicturePath = "/uploads/default.jpg",
                Role=dto.Role
            };
            user.Currency.userId = user.Id;
            user.Currency.sum = 0;


            return _repository.Register(user);
        }


        public async Task<User> Login(LoginDto dto)
        {
            var user = await _repository.Login(dto);
            var passwordHash = dto.Password.Length % 3 + 5.ToString();
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

    }


}

