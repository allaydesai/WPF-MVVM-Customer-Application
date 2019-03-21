using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDashboard.Services;
using ZzaDesktop;

namespace ZzaDashboard.Customers
{
    class AddEditCustomerViewModel : BindableBase
    {
        private bool _editMode;
        private ICustomersRepository _repo;

        public bool EditMode
        {
            get => _editMode;
            set => SetProperty(ref _editMode, value);
        }

        public AddEditCustomerViewModel(ICustomersRepository repo)
        {
            _repo = repo;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }

        private SimpleEditableCustomer _Customer;

        public SimpleEditableCustomer Customer
        {
            get { return _Customer; }
            set { SetProperty(ref _Customer, value);}
        }

        private Customer _editingCustomer = null;

        public void SetCustomer(Customer cust)
        {
            _editingCustomer = cust;
            if (Customer != null)
                Customer.ErrorsChanged -= RaiseCanExecutedChanged;
            Customer = new SimpleEditableCustomer();
            Customer.ErrorsChanged += RaiseCanExecutedChanged;
            CopyCustomer(cust, Customer);
        }

        private void RaiseCanExecutedChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }


        private void CopyCustomer(Customer source, SimpleEditableCustomer target)
        {
            target.Id = source.Id;
            if (EditMode)
            {
                target.FirstName = source.FirstName;
                target.LastName = source.LastName;
                target.Phone = source.Phone;
                target.Email = source.Email;
            }
        }

        private void UpdateCustomer(SimpleEditableCustomer source, Customer target)
        {
            
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            target.Phone = source.Phone;
            target.Email = source.Email;
            
        }

        public event Action Done = delegate { };

        public RelayCommand CancelCommand { get; private set; }
        private void OnCancel()
        {
            Done();
        }

        public RelayCommand SaveCommand { get; private set; }

        private bool CanSave()
        {
            return !Customer.HasErrors;
        }

        private async void OnSave()
        {
            UpdateCustomer(Customer, _editingCustomer);
            if (EditMode)
                await _repo.UpdateCustomerAsync(_editingCustomer);
            else
                await _repo.AddCustomerAsync(_editingCustomer);

            Done();
        }



        
    }
}
