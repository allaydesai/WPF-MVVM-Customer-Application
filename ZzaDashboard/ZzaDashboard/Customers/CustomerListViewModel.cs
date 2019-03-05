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
    public class CustomerListViewModel
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        public RelayCommand DeleteCommand { get; private set; }

        public CustomerListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
            // Async function returns a Task.result() gives list => List using which construct a collection
            Customers = new ObservableCollection<Customer>(_repo.GetCustomersAsync().Result);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        private void OnDelete()
        {
            Customers.Remove(SelectedCustomer);
        }

        public ObservableCollection<Customer> Customers { get => _customers; set => _customers = value; }

        
        public Customer SelectedCustomer {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
