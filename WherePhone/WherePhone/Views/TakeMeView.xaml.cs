﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WherePhone.ViewModels;
using Xamarin.Forms;

namespace WherePhone.Views
{
    public partial class TakeMeView : ContentPage
    {
        private TakeMeViewModel _viewModel;
        

        public TakeMeView(TakeMeViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.BindingContext = viewModel;
           
        }

    }
}
