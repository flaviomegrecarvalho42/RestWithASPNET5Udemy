using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Models;
using RestWithAspNet5Udemy.Models.Context;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNet5Udemy.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MSSQLContext _context;

        public UserRepository(MSSQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserDto userDto)
        {
            var passWord = ComputeHash(userDto.Password, new SHA256CryptoServiceProvider());
           
            return _context.Users.FirstOrDefault(u =>(u.UserName == userDto.UserName) &&
                                                     (u.Password == passWord));
        }

        public User RefreshCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public User RefreshUserInfo(User user)
        {
            // We check if the person exists in the database
            // If it doesn't exist we return an empty person instance
            if (!_context.Users.Any(p => p.Id.Equals(user.Id)))
                return null;

            try
            {
                // Get the current status of the record in the database
                var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

                if (result == null)
                    return user;

                // set changes and save
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);

            if (user == null)
                return false;

            user.RefreshToken = null;
            _context.SaveChanges();

            return true;

        }

        private static string ComputeHash(string password, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

    }
}
