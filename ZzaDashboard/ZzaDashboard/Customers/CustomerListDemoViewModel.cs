using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListDemoViewModel: INotifyPropertyChanged
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;

        //delegate trick where we assign an empty anonymous method in as a subscriber
        //That means that subscriber is always in the list, and you never have to worry about PropertyChanged being null
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public RelayCommand DeleteCommand { get; private set; }

        public CustomerListDemoViewModel()
        {
            //if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            //    return;
            //// Async function returns a Task.result() gives list => List using which construct a collection
            //Customers = new ObservableCollection<Customer>(_repo.GetCustomersAsync().Result);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        public async void LoadCustomer()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
            // Async function returns a Task.result() gives list => List using which construct a collection
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        private void OnDelete()
        {
            Customers.Remove(SelectedCustomer);
        }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    //raise property changed event
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
                }
                
            }
        }


        public Customer SelectedCustomer {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    DeleteCommand.RaiseCanExecuteChanged();
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCustomer"));
                }
                
                
            }
        }

    }
}
