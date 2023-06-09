using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;
        #endregion 

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                     IRefreshTokenRepository refreshTokenRepository,
                                     UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager= userManager;
        }


        #endregion

        #region Handle Functions

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) =GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate=DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed=true,
                IsRevoked=false,
                JwtId=jwtToken.Id,
                RefreshToken=refreshToken.TokenString,
                Token=accessToken,
                UserId=user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken=accessToken;
            return response;
        }

        private (JwtSecurityToken, string) GenerateJWTToken(User user)
        {
            var claims = GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName= username,
                TokenString=GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),

            };
            return claims;
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken)
        {
            //Read Token To get Cliams
            var jwtToken = ReadJWTToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Algorithm Is Wrong");
            }
            if (jwtToken.ValidTo>DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token Is not Expired");
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type==nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token==accessToken&&
                                                                     x.RefreshToken==refreshToken&&
                                                                     x.UserId==int.Parse(userId));
            if (userRefreshToken == null)
            {
                throw new SecurityTokenException("Refresh Token Is Not Found");
            }

            if (userRefreshToken.ExpiryDate<DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked=true;
                userRefreshToken.IsUsed=false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh Token Is Expired");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new SecurityTokenException("User Is Not Found");
            }
            var (jwtSecurityToken, newToken) = GenerateJWTToken(user);

            var response = new JwtAuthResult();
            response.AccessToken=newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName=jwtToken.Claims.FirstOrDefault(x => x.Type==nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString=refreshToken;
            refreshTokenResult.ExpireAt=userRefreshToken.ExpiryDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }
        private JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try
            {
                if (validator==null)
                {
                    throw new SecurityTokenException("Invalid Token");
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }
}
