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

         

            builder.RegisterType<ApiSettings>()
                .As<IApiSettings>()
                .SingleInstance();

            builder.RegisterType<ApiExecuter>()
               .As<IApiExecuter>()
               .SingleInstance();

            builder.RegisterType<ApiFacade>()
                .As<IApiFacade>()
                .SingleInstance();

         

            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<AllDevicesViewModel>().AsSelf();
            builder.RegisterType<TakeMeViewModel>().AsSelf();

            builder.RegisterType<MainView>()
              .AsSelf()
              .SingleInstance();

            builder.RegisterType<AllDevicesView>()
               .AsSelf()
               .SingleInstance();

            builder.RegisterType<TakeMeView>()
             .AsSelf()
             .SingleInstance();
        }
    }
}
