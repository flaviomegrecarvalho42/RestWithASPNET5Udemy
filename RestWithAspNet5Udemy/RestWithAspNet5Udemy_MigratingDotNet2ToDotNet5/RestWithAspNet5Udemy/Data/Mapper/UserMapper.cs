using System.Collections.Generic;
using RestWithAspNet5Udemy.Data.DTO;
using System.Linq;
using RestWithAspNet5Udemy.Data.Mapper.Interfaces;
using RestWithAspNet5Udemy.Models;

namespace RestWithAspNet5Udemy.Data.Mapper
{
    public class UserMapper : IParser<UserDto, User>, IParser<User, UserDto>
    {
        public User Parse(UserDto origin)
        {
            if (origin == null)
                return new User();
            
            return new User
            {
                Login = origin.Login,
                AccessKey = origin.AccessKey
            };
        }

        public UserDto Parse(User origin)
        {
            if (origin == null)
                return new UserDto();
            
            return new UserDto
            {
                Login = origin.Login,
                AccessKey = origin.AccessKey
            };
        }

        public List<User> ParseList(List<UserDto> origin)
        {
            if (origin == null)
                return new List<User>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<UserDto> ParseList(List<User> origin)
        {
            if (origin == null)
                return new List<UserDto>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
