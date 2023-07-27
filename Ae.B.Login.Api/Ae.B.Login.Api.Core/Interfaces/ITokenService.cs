using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Login.Api.Core.Model;

namespace Ae.B.Login.Api.Core.Interfaces
{
    public interface ITokenService
    {
        TokenInfo GenerateToken(EmployeeInfoVO emp, string refreshUri);

        bool ValidateToken(string token);

        TokenInfo RefreshToken(string refreshToken, string refreshUri);

    }
}
