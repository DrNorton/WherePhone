using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceInfo.Plugin.Abstractions;

namespace WherePhone
{
    public interface IAppIdGenerator
    {
        string Id();
        string DeviceModel();
        Platform Platform();
        string PlatformVersion();
    }
}
