using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZzaDesktop
{
    public class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        // Dictionary as our underlying datastore, that has a key of string, which is the property name, and a List of string per property,
        // which are the errors associated with that property
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };
        
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            // look into that dictionary when we're queried for a property,
            // if there's any errors in there, return them as a list of strings. 
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            else
                return null;
        }
        // Notifies if any errors on the class
        public bool HasErrors
        {
            // Checks if there is anything in the Dict or not
            get { return _errors.Count > 0; }
        }
        // trigger for when to evaluate errors, hence overriding set property
        protected override void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            base.SetProperty<T>(ref member, val, propertyName);
            ValidateProperty(propertyName, val);
        }

        //Data Annotation for validation
        private void ValidateProperty<T>(string propertyName, T value)
        {
            //ValidationContext that you can point at a given object, say what member or property on that object is being validated,
            var results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(this);
            context.MemberName = propertyName;
            //and then call a method to evaluate that object
            // check if there's any data annotation attributes for validation
            Validator.TryValidateProperty(value, context, results);

            if (results.Any())
            {

                _errors[propertyName] = results.Select(c => c.ErrorMessage).ToList();
            }
            else
            {
                _errors.Remove(propertyName);
            }
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }


    }
}
