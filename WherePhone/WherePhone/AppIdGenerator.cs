using DeviceInfo.Plugin;
using DeviceInfo.Plugin.Abstractions;

///http://codeworks.it/blog/?p=260
namespace WherePhone
{
    public class AppIdGenerator :IAppIdGenerator
    {
        public string Id()
        {
            return CrossDeviceInfo.Current.Id;
        }

        public string DeviceModel()
        {
            return CrossDeviceInfo.Current.Model;
        }

        public Platform Platform()
        {
            return CrossDeviceInfo.Current.Platform;
        }

        public string PlatformVersion()
        {
            return CrossDeviceInfo.Current.Version;
        }
    }
}