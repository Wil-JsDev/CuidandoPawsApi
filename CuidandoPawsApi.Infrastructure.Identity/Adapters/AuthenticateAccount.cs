﻿using CuidandoPawsApi.Application.DTOs.Account.Authenticate;
using CuidandoPawsApi.Application.DTOs.Account.JWT;
using CuidandoPawsApi.Application.DTOs.Account.Register;
using CuidandoPawsApi.Domain.Ports.UseCase.Account;
using CuidandoPawsApi.Domain.Settings;
using CuidandoPawsApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CuidandoPawsApi.Infrastructure.Identity.Adapters
{
    public class AuthenticateAccount : IAuthenticateAccount<AuthenticateResponse, AuthenticateRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private JWTSettings _JWTSettings;

        public AuthenticateAccount(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JWTSettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _JWTSettings = options.Value;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request)
        {

            AuthenticateResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenereteToken(user);

            response.Id = user.Id;
            response.Username = user.UserName;
            response.Email = user.Email;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        private async Task<JwtSecurityToken> GenereteToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), //El dueño de ese token (sub)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //Es un identificador unico, es como el ID del token
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id), //Claim personalizado
            }
            .Union(userClaims)
            .Union(roleClaims);

            //Generar llave simetrica de seguridad
            var symmectricSecutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecutityKey, SecurityAlgorithms.HmacSha256);

            //Este es el token simetrica 
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _JWTSettings.Issuer,
                audience: _JWTSettings.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(_JWTSettings.DurationInMinutes),
                signingCredentials: signingCredetials
            );

            return jwtSecurityToken;
        }


        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expired = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomByte = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomByte);

            return BitConverter.ToString(randomByte).Replace("-", "");
        }
    }
}