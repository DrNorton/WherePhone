using System;
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

        public static void Run(Application app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<Installer>();
            var container = builder.Build();
            IoC.Init(container);
            var page = container.Resolve<MainView>();
            app.MainPage = new NavigationPage(page);
        }
    }

    public static class IoC
    {
        private static IContainer _container;

        public static void Init(IContainer cnContainer)
        {
            _container = cnContainer;
        }

        public static T Get<T>()
        {
           return _container.Resolve<T>();
        }
    }
}
