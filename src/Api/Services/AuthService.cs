using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Codidact.Core.Application.Common.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Codidact.Core.Api.Services
{
    public class AuthService : Auth.AuthBase
    {
        private readonly ISecretsService _secretsService;
        private readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
        private readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();


        public AuthService(ISecretsService secretsService)
        {
            _secretsService = secretsService;
        }
        public async override Task<AuthResponse> AuthorizeClient(ClientAuthRequest request, ServerCallContext context)
        {
            if (await _secretsService.Get("Api:ClientId") == request.ClientId
                && await _secretsService.Get("Api:ClientSecret") == request.ClientSecret)
            {
                var claims = new[] { new Claim("codidact_member_id", "0") };
                var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
                return new AuthResponse
                {
                    Token = JwtTokenHandler.WriteToken(token)
                };
            }
            context.Status = new Status(StatusCode.Unauthenticated, "invalid grant");
            return null;
        }
    }
}
