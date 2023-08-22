using Ae.FileUpload.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FileUpload.Api.Core.Interfaces
{
    public interface ISwAlarmService
    {
         Task AlarmReceive(List<SwAlarmRequest> alarmList);
    }
}
