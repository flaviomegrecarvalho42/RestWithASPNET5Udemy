using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Configurations;
using RestWithAspNet5Udemy.Data.DTO;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using RestWithAspNet5Udemy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithAspNet5Udemy.BLL
{
    public class LoginBLL : ILoginBLL
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly TokenConfiguration _configuration;
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBLL(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenDto ValidateCredentials(UserDto userDto)
        {
            //Pegar as credenciais e validar no Banco de Dados
            var user = _repository.ValidateCredentials(userDto);

            //Se não for validado, retornará nulo
            if (user == null)
                return null;

            //Caso as credenciais estejam válidas serão geradas as Claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            //Caso as credenciais estejam válidas serão geradas os Tokens (de acesso e de refresh)
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            //Setar os tokens no usuário recuperado no Banco de Dados
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);
            
            //Em seguida, os dados atualizados do usuário são gravados no Banco de Dados
            _repository.RefreshUserInfo(user);

            //Em seguida, serão criadas a data de criação e expiração do Token
            //Por fim, as informações do Token são setadas em uma variável e retornadas para o Controller e cliente
            return GetNewTokenDto(accessToken, refreshToken);
        }

        public TokenDto RefreshCredentials(TokenDto tokenDto)
        {
            var accessToken = tokenDto.AccessToken;
            var refreshToken = tokenDto.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;
            var user = _repository.RefreshCredentials(userName);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            return GetNewTokenDto(accessToken, refreshToken);
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }

        private TokenDto GetNewTokenDto(string accessToken, string refreshToken)
        {
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);
           
            var tokenDto = new TokenDto(true,
                                        createDate.ToString(DATE_FORMAT),
                                        expirationDate.ToString(DATE_FORMAT),
                                        accessToken,
                                        refreshToken);
            
            return tokenDto;
        }
    }
}
