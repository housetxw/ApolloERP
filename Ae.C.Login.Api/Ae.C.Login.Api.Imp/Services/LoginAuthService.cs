using Ae.C.Login.Api.Dal.Repositorys.User;
using Ae.C.Login.Api.Dal.Repositorys.UserThird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.C.Login.Api.Core.Model;
using AutoMapper;
using System.Threading.Tasks;
using Ae.C.Login.Api.Core.Request;
using Ae.C.Login.Api.Core.Interfaces;
using ApolloErp.Log;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Ae.C.Login.Api.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Ae.C.Login.Api.Imp.Services
{
    public class LoginAuthService : ILoginAuthService
    {
        private readonly ApolloErpLogger<LoginAuthService> logger;
        private IOptions<JwtConfig> options;

        public LoginAuthService(ApolloErpLogger<LoginAuthService> logger, IOptions<JwtConfig> options)
        {
            this.logger = logger;
            this.options = options;
        }

        /// <summary>
        /// 颁发token信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        public TokenInfoVO GetTokenInfo(UserInfoVO user, string refreshUri)
        {
            string nickName = user?.NickName;
            if(string.IsNullOrEmpty(nickName))
            {
                nickName = user?.MobileNumber ?? string.Empty;
            }
            //AccessToken身份信息
            Claim[] claimsAccessToken = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user?.UserId??string.Empty),
                new Claim(ClaimTypes.Name, nickName),
                //new Claim(ClaimTypes.Name, user?.OpenId??string.Empty),
                //new Claim(ClaimTypes.MobilePhone, user?.MobileNumber??string.Empty)
            };
            //RefreshToken身份信息
            Claim[] claimsRefreshToken = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user?.UserId??string.Empty),
                new Claim(ClaimTypes.Name, nickName),
            };
            TokenInfoVO token = new TokenInfoVO();
            DateTime now = DateTime.Now;
            //创建AccessToken
            Token tokenA = CreateToken(now, claimsAccessToken, TokenType.AccessToken);
            token.AccessToken = tokenA.TokenContent;
            token.Expires = tokenA.Expires;

            //创建RefreshToken
            Token tokenR = CreateToken(now, claimsRefreshToken, TokenType.RefreshToken);
            token.RefreshToken = tokenR.TokenContent;

            token.RefreshUri = refreshUri;
            return token;
        }

        /// <summary>
        /// 刷新token获取新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        public TokenInfoVO RefreshToken(string refreshToken, string refreshUri)
        {
            TokenInfoVO token = new TokenInfoVO();
            UserInfoVO user = new UserInfoVO();
            //检测token是否有效
            bool isAuthenticated = ValidateToken(refreshToken,out user);
            //无效直接返回空
            if(!isAuthenticated)
            {
                return token;
            }
            //有效的情况下生成新的token对象
            token = GetTokenInfo(user, refreshUri);
            return token;
        }

        /// <summary>
        /// 用于创建AccessToken和RefreshToken
        /// </summary>
        /// <param name="now"></param>
        /// <param name="claims"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        private Token CreateToken(DateTime now, Claim[] claims, TokenType tokenType)
        {
            var expires = now.Add(TimeSpan.FromMinutes(tokenType.Equals(TokenType.AccessToken) ? options.Value.AccessTokenExpiresMinutes : options.Value.RefreshTokenExpiresMinutes));
            var token = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: tokenType.Equals(TokenType.AccessToken) ? options.Value.Audience : options.Value.RefreshTokenAudience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.IssuerSigningKey)), SecurityAlgorithms.HmacSha256));
            Token tokenResult = new Token();
            tokenResult.TokenContent = new JwtSecurityTokenHandler().WriteToken(token);
            tokenResult.Expires = expires;
            return tokenResult;
        }

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private  ClaimsPrincipal GetPrincipal(string token)
        { 
            // 此方法用解码字符串token，并返回秘钥的信息对象
            try
            {
                // 创建一个JwtSecurityTokenHandler类，用来后续操作
                var tokenHandler = new JwtSecurityTokenHandler();

                // 生成验证token的参数
                var validationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = options.Value.Issuer,
                    ValidAudience = options.Value.RefreshTokenAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.IssuerSigningKey)),
                };
                SecurityToken securityToken; // 接受解码后的token对象
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal; // 返回秘钥的主体对象，包含秘钥的所有相关信息
            }
            catch (Exception ex)
            {
                logger.Error("解析RefreshToken失败", ex);
                return null;
            }
        }
        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool ValidateToken(string token, out UserInfoVO user)
        {
            user = null;
            // 调用自定义的GetPrincipal获取Token的信息对象
            var simplePrinciple = GetPrincipal(token);
            // 获取主声明标识
            var identity = simplePrinciple?.Identity as ClaimsIdentity; 
            if (identity == null) 
                return false;
            if (!identity.IsAuthenticated) 
                return false;
            // 获取声明类型是ClaimTypes.NameIdentifier的第一个声明
            var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            // 获取声明的名字，也就是用户名
            var userId = userIdClaim?.Value; 
            if (string.IsNullOrEmpty(userId)) 
                return false;
            // 获取声明类型是ClaimTypes.Name
            var nameClaim = identity.FindFirst(ClaimTypes.Name);
            var name = nameClaim?.Value;
            if (string.IsNullOrEmpty(name))
                return false;
            user.UserId = userId;
            user.NickName = name;
            return true;

        }



    }
}
