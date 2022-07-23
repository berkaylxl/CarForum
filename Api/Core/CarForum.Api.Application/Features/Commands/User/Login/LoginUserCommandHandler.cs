using AutoMapper;
using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Exceptions;
using CarForum.Common.Infrastructure;
using CarForum.Common.Models.Queries;
using CarForum.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetSingleAsync(i=>i.EmailAdress==request.EmailAdress);

            if (dbUser == null)
                throw new DatabaseValidationExcepton("User not found");

            var password = PasswordEncryptor.Encrpt(request.Password);
            if (dbUser.Password != password)
                throw new DatabaseValidationExcepton("Password is wrong");

            if (dbUser.EmailConfirmed)
                throw new DatabaseValidationExcepton("Email is not confirmed");

            var result = mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email,dbUser.EmailAdress),
                new Claim(ClaimTypes.Name,dbUser.Username),
                new Claim(ClaimTypes.GivenName,dbUser.FirstName),
                new Claim(ClaimTypes.Surname,dbUser.LastName),
            };


            result.Token = GenerateToken(claims);
            return result;
          
        }
        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var creds =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiry=DateTime.UtcNow.AddDays(10);

            var token=new JwtSecurityToken(claims:claims,
                                           expires:expiry,
                                           signingCredentials:creds,
                                           notBefore:DateTime.Now);
            return new JwtSecurityTokenHandler().WriteToken(token);
                                          
        
        }
    }
}
