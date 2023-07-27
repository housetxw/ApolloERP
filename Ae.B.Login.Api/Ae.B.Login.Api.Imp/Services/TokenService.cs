using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.B.Login.Api.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ApolloErp.Log;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Core.Model;
using Ae.B.Login.Api.Core.Response;

namespace Ae.B.Login.Api.Imp.Services
{
    public class TokenService : ITokenService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<TokenService> logger;

        private readonly IMapper mapper;

        private readonly IOptions<JwtConfig> options;

        public TokenService(ApolloErpLogger<TokenService> logger, IMapper mapper, IOptions<JwtConfig> options)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.options = options;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        public TokenInfo GenerateToken(EmployeeInfoVO emp, string refreshUri)
        {
            //AccessToken身份信息
            Claim[] claimAccessToken = {
                new Claim(ClaimTypes.NameIdentifier, emp.EmployeeId ?? string.Empty),
                new Claim(ClaimTypes.Name, emp.EmployeeName ?? string.Empty),
                new Claim(ClaimTypes.Surname, Convert.ToInt32(emp.EmployeeType).ToString()),
                new Claim(ClaimTypes.GroupSid, Convert.ToInt32(emp.OrganizationId).ToString())
            };

            //RefreshToken身份信息
            Claim[] claimRefreshToken = {
                new Claim(ClaimTypes.NameIdentifier, emp.EmployeeId ?? string.Empty),
                new Claim(ClaimTypes.Name, emp.EmployeeName ?? string.Empty),
                new Claim(ClaimTypes.Surname, Convert.ToInt32(emp.EmployeeType).ToString()),
                new Claim(ClaimTypes.GroupSid, Convert.ToInt32(emp.OrganizationId).ToString())
            };

            var token = new TokenInfo();
            DateTime now = DateTime.Now;

            //创建AccessToken
            Token tokenA = CreateToken(now, claimAccessToken, TokenType.AccessToken);
            token.AccessToken = tokenA.TokenContent;
            token.Expires = tokenA.Expires;

            //创建RefreshToken
            Token tokenR = CreateToken(now, claimRefreshToken, TokenType.RefreshToken);
            token.RefreshToken = tokenR.TokenContent;
            token.RefreshUri = refreshUri;

            return token;
        }

        /// <summary>
        /// 根据RefreshToken获取新的AccessToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="refreshUri"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public TokenInfo RefreshToken(string refreshToken, string refreshUri)
        {
            EmployeeInfoVO emp = new EmployeeInfoVO();

            //检测token是否有效
            bool isAuthenticated = ValidateRefreshToken(refreshToken, ref emp);

            //无效直接返回空
            if (!isAuthenticated)
            {
                return null;
            }

            //AccessToken：存放empId，orgId和orgType
            //RefreshToken：只存放empId，
            //有效的情况下生成新的token对象
            var token = GenerateToken(emp, refreshUri);
            return token;
        }

        /// <summary>
        /// 验证AccessToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool ValidateToken(string token)
        {
            // 调用自定义的GetPrincipal获取Token的信息对象
            var simplePrinciple = GetPrincipal(token, TokenType.AccessToken);

            // 获取主声明标识
            ClaimsIdentity identity = simplePrinciple?.Identity as ClaimsIdentity;
            if (null == identity || !identity.IsAuthenticated) { return false; }

            return identity.IsAuthenticated;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        /// <summary>
        /// 用于创建AccessToken和RefreshToken
        /// </summary>
        /// <param name="now"></param>
        /// <param name="claims"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        private Token CreateToken(DateTime now, Claim[] claims, TokenType tokenType)
        {
            var expires = now.Add(
                TimeSpan.FromMinutes(tokenType.Equals(TokenType.AccessToken)
                ? options.Value.AccessTokenExpiresMinutes
                : options.Value.RefreshTokenExpiresMinutes));

            var token = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: tokenType.Equals(TokenType.AccessToken)
                    ? options.Value.AccessTokenAudience
                    : options.Value.RefreshTokenAudience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.IssuerSigningKey)),
                    SecurityAlgorithms.HmacSha256));

            Token tokenRes = new Token
            {
                TokenContent = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = Convert.ToInt32(options.Value.AccessTokenExpiresMinutes) * 60
            };
            return tokenRes;
        }

        /// <summary>
        /// 验证RefreshToken
        /// </summary>
        /// <param name="token"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        private bool ValidateRefreshToken(string token, ref EmployeeInfoVO emp)
        {
            // 调用自定义的GetPrincipal获取Token的信息对象
            var simplePrinciple = GetPrincipal(token, TokenType.RefreshToken);

            // 获取主声明标识
            ClaimsIdentity identity = simplePrinciple?.Identity as ClaimsIdentity;
            if (null == identity) { return false; }

            if (!identity.IsAuthenticated) { return false; }

            // 获取声明类型是ClaimTypes.NameIdentifier的第一个声明，EmployeeId
            var empIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            emp.EmployeeId = empIdClaim?.Value;

            // 获取声明类型是ClaimTypes.Name，EmployeeName
            var empNameClaim = identity.FindFirst(ClaimTypes.Name);
            emp.EmployeeName = empNameClaim?.Value;

            // 获取声明类型是ClaimTypes.Surname，EmployeeType
            var orgTypeClaim = identity.FindFirst(ClaimTypes.Surname);
            emp.EmployeeType = (EmployeeType)Convert.ToInt32(orgTypeClaim?.Value);

            // 获取声明类型是ClaimTypes.GroupSid，OrganizationId
            var orgIdClaim = identity.FindFirst(ClaimTypes.GroupSid);
            emp.OrganizationId = Convert.ToInt32(orgIdClaim?.Value);

            return emp.EmployeeId.IsNotNullOrWhiteSpace()
                   && emp.EmployeeName.IsNotNullOrWhiteSpace()
                   && emp.OrganizationId > 0
                   && emp.EmployeeType != EmployeeType.None;
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipal(string token, TokenType tokenType)
        {
            // 此方法用解码字符串token，并返回秘钥的信息对象
            try
            {
                // 创建一个JwtSecurityTokenHandler类，用来后续操作
                var tokenHandler = new JwtSecurityTokenHandler();

                // 生成验证token的参数
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = options.Value.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.IssuerSigningKey)),
                    ValidAudience = tokenType.Equals(TokenType.AccessToken)
                        ? options.Value.AccessTokenAudience
                        : options.Value.RefreshTokenAudience
                };

                // 返回秘钥的主体对象，包含秘钥的所有相关信息
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                return principal;
            }
            catch (SecurityTokenExpiredException ex)
            {
                logger.Warn($"\n【{tokenType.GetEnumDescription()} 过期】\n", ex);
                return null;
            }
            catch (Exception ex)
            {
                var tokenDesc = tokenType.GetEnumDescription();
                logger.Error($"\n【解析 {tokenDesc} 失败({tokenDesc}过期)】\n", ex);
                return null;
            }
        }

    }
}
