using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzaDesktop;

namespace ZzaDashboard.Orders
{
    class OrderViewModel : BindableBase
    {
        private Guid _customerId;

        public Guid CustomerId
        {
            get => _customerId;
            set => SetProperty(ref _customerId, value);
        }
    }
}
