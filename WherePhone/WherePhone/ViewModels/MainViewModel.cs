﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeviceInfo.Plugin;
using DeviceInfo.Plugin.Abstractions;
using WherePhone.Api;
using WherePhone.Api.Facade;
using WherePhone.Api.Models;
using WherePhone.Views;
using Xamarin.Forms;
using Device = WherePhone.Api.Models.Device;

namespace WherePhone.ViewModels
{
  
    public class MainViewModel :BaseViewModel
    {
       
        private List<MenuItem> _menuItems;
        private MenuItem _selectedMenu;
     
        public  INavigation Navigation { get; set; }


        public MainViewModel()
        {
         
            MenuItems = new List<MenuItem>() { new MenuItem() {Action = "phone",Description = "узнать у кого телефон", Name = "Где телефон" },
                 new MenuItem(){Action = "take",Description = "выбрав фамилию из списка",Name = "Взять телефон"},
                 new MenuItem(){Action = "phoneInformation",Description = "информация о телефоне",Name = "О телефоне"},
                 new MenuItem(){Action = "locator",Description = "поиск телефонов",Name = "Локатор телефонов"},
            };
            
        }

        

        

      

        public List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                base.OnPropertyChanged("MenuItems");
            }
        }

        public MenuItem SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                _selectedMenu = value;
                if (value != null)
                {
                    GoTo(value.Action);
                }
            
                
                base.OnPropertyChanged("SelectedMenu");
            }
        }

        private void GoTo(string action)
        {
            switch (action)
            {
                case "phone":
                    Navigation.PushAsync(IoC.Get<PhoneOwnerView>());
                    break;

                case "phoneInformation":
                    Navigation.PushAsync(IoC.Get<DeviceInfoView>());
                    break;

                case "take":
                    Navigation.PushAsync(IoC.Get<TakeMeView>());
                    break;

                case "locator":
                    Navigation.PushAsync(IoC.Get<PhoneLocatorView>());
                    break;
            }
           
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Action { get; set; }
    }
}
