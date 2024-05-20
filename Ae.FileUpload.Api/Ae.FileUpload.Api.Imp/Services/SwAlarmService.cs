using Microsoft.Extensions.Configuration;
using MimeKit;
using ApolloErp.Email.Message;
using ApolloErp.Message.Sms;
using Ae.FileUpload.Api.Core.Interfaces;
using Ae.FileUpload.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FileUpload.Api.Imp.Services
{
    public class SwAlarmService: ISwAlarmService
    {
        private readonly EMailClient eMailClient;
        private readonly IConfiguration configuration;
        public SwAlarmService(EMailClient eMailClient, IConfiguration configuration)
        {
            this.eMailClient = eMailClient;
            this.configuration = configuration;
        }

        public async Task AlarmReceive(List<SwAlarmRequest> alarmList)
        {
            string toString = configuration["ToSwAlarmEamil"];
            List<MailboxAddress> toList = new List<MailboxAddress>();
            string[] toArr = toString.Split(",");
            foreach(var i in toArr)
            {
                MailboxAddress to = new MailboxAddress("aerp",i);
                toList.Add(to);
            }
            string msg = GetContent(alarmList);
            //注入方法调用
            await eMailClient.SendEMailAsync($"系统监控报警-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}",msg,toList);
        }

        private string GetContent(List<SwAlarmRequest> alarmList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SwAlarmRequest dto in alarmList)
            {
                sb.Append("scopeId: ").Append(dto.scopeId)
                        .Append("\nscope: ").Append(dto.scope)
                        .Append("\n目标 Scope 的实体名称: ").Append(dto.name)
                        .Append("\nScope 实体的 ID: ").Append(dto.id0)
                        .Append("\nid1: ").Append(dto.id1)
                        .Append("\n告警规则名称: ").Append(dto.ruleName)
                        .Append("\n告警消息内容: ").Append(dto.alarmMessage)
                        .Append("\n告警时间: ").Append(dto.startTime)
                        .Append("\n\n---------------\n\n");
            }

            return sb.ToString();
        }
    }
}
