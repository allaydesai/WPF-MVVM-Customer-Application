﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZzaDashboard.Customers;
using ZzaDashboard.OrderPrep;
using ZzaDashboard.Orders;
using ZzaDesktop;

namespace ZzaDashboard
{
    public class MainWindowViewModel : BindableBase
    {
        //private Timer _timer = new Timer(5000); //5s

        private CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();

        private BindableBase _CurrentViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new RelayCommand<string>(OnNav);
        }

        public BindableBase CurrentViewModel
        {
            get => _CurrentViewModel;
            set => SetProperty(ref _CurrentViewModel, value);
        }

        public RelayCommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;
                case "customer":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;

            }
        }



        //!!!! OLD CODE!!!
        //public MainWindowViewModel()
        //{
        //    //CurrentViewModel = new CustomerListViewModel();
        //    //_timer.Elapsed += (s, e) => NotificationMessage = "At the tone time will be: " + DateTime.Now.ToLocalTime();
        //    //_timer.Start();
            
        //    //NotificationMessage = "At the tone time will be: " + DateTime.Now.ToLocalTime();

        //}

        //private string _NotificationMessage;

        //public string NotificationMessage
        //{
        //    get => _NotificationMessage;
        //    set
        //    {
        //        if (value == _NotificationMessage) return;
        //        _NotificationMessage = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("NotificationMessage"));
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
