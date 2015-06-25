using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

using WherePhone.Api;
using WherePhone.Api.Executer;
using WherePhone.Api.Facade;

using WherePhone.ViewModels;
using WherePhone.Views;

namespace WherePhone
{
    public class Installer : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppIdGenerator>()
                   .As<IAppIdGenerator>()
                   .SingleInstance();

            builder.RegisterType<ApiSettings>()
                .As<IApiSettings>()
                .SingleInstance();

            builder.RegisterType<ApiExecuter>()
               .As<IApiExecuter>()
               .SingleInstance();

            builder.RegisterType<ApiFacade>()
                .As<IApiFacade>()
                .SingleInstance();



            builder.RegisterType<MainViewModel>().InstancePerDependency();
            builder.RegisterType<TakeMeViewModel>().InstancePerDependency();
            builder.RegisterType<PhoneOwnerViewModel>().InstancePerDependency();
            builder.RegisterType<DeviceInfoViewModel>().InstancePerDependency();
            builder.RegisterType<PhoneOwnerViewModel>().InstancePerDependency();
            builder.RegisterType<PhoneLocatorViewModel>().InstancePerDependency();
            builder.RegisterType<PhoneLocatorDetailViewModel>().InstancePerDependency();

            builder.RegisterType<MainView>().InstancePerDependency();
            builder.RegisterType<TakeMeView>().InstancePerDependency();
            builder.RegisterType<DeviceInfoView>().InstancePerDependency();
            builder.RegisterType<PhoneOwnerView>().InstancePerDependency();
            builder.RegisterType<PhoneLocatorView>().InstancePerDependency();
            builder.RegisterType<PhoneLocatorDetailView>().InstancePerDependency();
        }
    }
}
