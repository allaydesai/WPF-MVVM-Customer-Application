using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Services;

namespace ZzaDashboard.Customers
{
    public class CustomerListViewModel
    {
        private ICustomersRepository _repo = new CustomersRepository();
        private ObservableCollection<Customer> _customers;

        public CustomerListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;
            // Async function returns a Task.result() gives list => List using which construct a collection
            Customers = new ObservableCollection<Customer>(_repo.GetCustomersAsync().Result);
        }

        public ObservableCollection<Customer> Customers { get => _customers; set => _customers = value; }
    }
}
