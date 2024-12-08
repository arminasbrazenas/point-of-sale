using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.ApplicationUserManagement.Interfaces;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

public class ApplicationUserAuthorizationService : IApplicationUserAuthorizationService
{
    private readonly IConfiguration _configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApplicationUserAuthorizationService(
        IConfiguration configuration,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork
    )
    {
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public string GetApplicationUserAccessToken(ApplicationUser user, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, role),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            ),
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public async Task<string> GetApplicationUserRefreshToken(ApplicationUser user, string role)
    {
        var refreshTokenValue = GenerateRefreshToken();
        var hashedRefreshToken = HashRefreshToken(refreshTokenValue);

        var refreshToken = new RefreshToken
        {
            RefreshTokenHash = hashedRefreshToken,
            ApplicationUserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:RefreshTokenExpirationTime"]!)),
            IsRevoked = false,
        };

        _refreshTokenRepository.Add(refreshToken);
        await _unitOfWork.SaveChanges();

        return refreshTokenValue;
    }

    public Task<string> RevokeApplicationUserRefreshToken(ApplicationUser user, string role, string refreshToken)
    {
        throw new NotImplementedException();
    }

    private string GenerateRefreshToken()
    {
        // TODO sign with HMAC512
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private string HashRefreshToken(string refreshToken)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    private async Task RevokeRefreshToken(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        _refreshTokenRepository.Update(refreshToken);
        await _unitOfWork.SaveChanges();
    }
}