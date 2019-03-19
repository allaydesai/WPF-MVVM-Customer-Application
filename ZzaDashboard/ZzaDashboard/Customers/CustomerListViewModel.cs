using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Services;
using ZzaDesktop;

namespace ZzaDashboard.Customers
{
    class CustomerListViewModel : BindableBase
    {
        private ICustomersRepository _repo = new CustomersRepository();

        public CustomerListViewModel()
        {
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
        }

   

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public async void LoadCustomers()
        {
            Customers = new ObservableCollection<Customer>(await _repo.GetCustomersAsync());
        }

        public RelayCommand<Customer> PlaceOrderCommand { get; private set; }
        // Declare event raised by this view model
        public event Action<Guid> PlaceOrderRequested = delegate { };

        private void OnPlaceOrder(Customer customer)
        {
            PlaceOrderRequested(customer.Id);
        }

        public RelayCommand AddCustomerCommand { get; private set; }
        public event Action<Customer> AddCustomerRequested = delegate { };

        private void OnAddCustomer()
        {
            AddCustomerRequested(new Customer {Id = Guid.NewGuid()});
        }

        public RelayCommand<Customer> EditCustomerCommand { get; private set; }
        public event Action<Customer> EditCustomerRequested = delegate { };

        private void OnEditCustomer(Customer cust)
        {
            EditCustomerRequested(cust);
        }
    }
}
