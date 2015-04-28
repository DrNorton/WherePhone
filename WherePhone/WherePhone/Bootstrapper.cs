﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WherePhone.Views;
using Xamarin.Forms;

namespace WherePhone
{
    public class Bootstrapper
    {
        public static void Run(Application app, IAppIdGenerator generator)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<Installer>();

            builder.RegisterInstance(generator)
             .As<IAppIdGenerator>()
             .SingleInstance();

            var container = builder.Build();

            var page = container.Resolve<MainView>();
            app.MainPage = new NavigationPage(page);
        }
    }
}
