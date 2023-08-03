using Ae.C.Login.Api.Client.Request;
using Ae.C.Login.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.Login.Api.Client.Clients.WeChatService
{
    public interface IWeChatClient
    {
        Task<GetJsCodeResponse> GetJscode2session(LoginPlatform platform , string jsCode);

        Task<object> GetWxacodeScene(GetWxacodeSceneRequest req);
    }
}
