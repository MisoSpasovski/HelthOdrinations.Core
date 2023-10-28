using System;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelthOrdinations.Core.Models;

namespace HelthOrdinations.Core.Helpers.Auth
{
    public class JWTTokenHelper
    {
        private string _tokenSecret = "P$cau!9&t5K9wXVjmuvNjh";
        private string _tokenIssuer = "https://localhost:7056";
        private string _tokenAudience = "https://localhost:7056";
        private int _tokenExpiration = 15;

        private SymmetricSecurityKey _tokenSecurityKey;
        private JWTTokenConfig _jwtTokenConfig;

        public JWTTokenHelper(IOptions<JWTTokenConfig> jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig.Value;

            _tokenSecret = _jwtTokenConfig.TokenSecret;
            _tokenIssuer = _jwtTokenConfig.TokenIssuer;
            _tokenAudience = _jwtTokenConfig.TokenAudience;
            _tokenExpiration = _jwtTokenConfig.TokenExpiration;

            _tokenSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSecret));
        }

        public string GenerateToken(UserInfo userInfoModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(userInfoModel),
                Expires = DateTime.UtcNow.AddHours(_tokenExpiration),
                Issuer = _tokenIssuer,
                Audience = _tokenAudience,
                SigningCredentials = new SigningCredentials(_tokenSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateClientToken(ClientsInfo clientInfoModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClientClaimsIdentity(clientInfoModel),
                Expires = DateTime.UtcNow.AddHours(_tokenExpiration),
                Issuer = _tokenIssuer,
                Audience = _tokenAudience,
                SigningCredentials = new SigningCredentials(_tokenSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GetClaimsIdentity(UserInfo userInfoModel)
        {
            var claimsIdentity = new ClaimsIdentity();
            if (userInfoModel != null)
            {
                if (userInfoModel.Email != null)
                {
                    claimsIdentity.AddClaim(new Claim(CustomClaimTypes.UserId, userInfoModel.Id.ToString()));
                }

                if (userInfoModel.UserName != null)
                {
                    claimsIdentity.AddClaim(new Claim(CustomClaimTypes.UserName, userInfoModel.UserName));
                }


                if (userInfoModel.Email != null)
                {
                    claimsIdentity.AddClaim(new Claim(CustomClaimTypes.UserEmail, userInfoModel.Email));
                }

            }

            return claimsIdentity;
        }

        private ClaimsIdentity GetClientClaimsIdentity(ClientsInfo clientInfoModel)
        {
            var claimsIdentity = new ClaimsIdentity();
            if (clientInfoModel != null)
            {
                if (clientInfoModel.Email != null)
                {
                    claimsIdentity.AddClaim(new Claim(CustomClaimTypes.UserId, clientInfoModel.Id.ToString()));
                }


                if (clientInfoModel.Email != null)
                {
                    claimsIdentity.AddClaim(new Claim(CustomClaimTypes.UserEmail, clientInfoModel.Email));
                }

            }

            return claimsIdentity;
        }

        public List<Claim> GetClaims(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();

            if (string.IsNullOrEmpty(jwtToken))
            {
                return new List<Claim>();
            }

            jwtToken = jwtToken.Replace("Bearer", "");
            var token = handler.ReadJwtToken(jwtToken.Trim());

            return token.Claims.ToList();
        }

        public bool ValidateCurrentToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _tokenIssuer,
                    ValidAudience = _tokenAudience,
                    IssuerSigningKey = _tokenSecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public UserInfo GetAuthUserInfo(string token)
        {
            UserInfo userInfoModel = new UserInfo();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var idClaim = securityToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserId);
            userInfoModel.Id = idClaim == null ? 0 : int.Parse(idClaim.Value);

            var userEmail = securityToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserEmail);
            userInfoModel.Email = userEmail == null ? string.Empty : userEmail.Value;

            //var emailClaim = securityToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserLastName);
            //userInfoModel.LastName = emailClaim == null ? string.Empty : emailClaim.Value;

            //var userRoleClaim = securityToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserRole);
            //userInfoModel.RoleId = int.Parse(userRoleClaim == null ? string.Empty : userRoleClaim.Value);

            return userInfoModel;
        }

        public ClientsInfo GetAuthClientInfo(string token)
        {
            ClientsInfo clientInfoModel = new ClientsInfo();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var idClaim = securityToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserId);
            clientInfoModel.Id = idClaim == null ? 0 : int.Parse(idClaim.Value);

            var userEmail = securityToken.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.UserEmail);
            clientInfoModel.Email = userEmail == null ? string.Empty : userEmail.Value;

            return clientInfoModel;
        }
    }
}

