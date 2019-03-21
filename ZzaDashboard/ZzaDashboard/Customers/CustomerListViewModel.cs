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
        private ICustomersRepository _repo;
        private List<Customer> _allCustomers;

        public CustomerListViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            PlaceOrderCommand = new RelayCommand<Customer>(OnPlaceOrder);
            AddCustomerCommand = new RelayCommand(OnAddCustomer);
            EditCustomerCommand = new RelayCommand<Customer>(OnEditCustomer);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        


        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        private string _searchInput;

        public string SearchInput
        {
            get => _searchInput;
            set
            {
                SetProperty(ref _searchInput, value);
                FilterCustomers(_searchInput);
            }
        }

        private void FilterCustomers(string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                Customers = new ObservableCollection<Customer>(_allCustomers);
                return;
            }
            else
            {
                Customers = new ObservableCollection<Customer>(_allCustomers.Where(c => c.FullName.ToLower().Contains(searchInput.ToLower())));
            }
        }
    

        public async void LoadCustomers()
        {
            _allCustomers = await _repo.GetCustomersAsync();
            Customers = new ObservableCollection<Customer>(_allCustomers);
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

        public RelayCommand ClearSearchCommand { get; private set; }
        private void OnClearSearch()
        {
            SearchInput=null;
        }
    }
}
