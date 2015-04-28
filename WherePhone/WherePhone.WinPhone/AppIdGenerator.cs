using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WherePhone.WinPhone
{
    public class AppIdGenerator : IAppIdGenerator
    {
        public string GetIdentifier()
        {
            byte[] myDeviceId = (byte[])Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId");
            return Convert.ToBase64String(myDeviceId);
        }
    }
}
