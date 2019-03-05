using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZzaDashboard
{
    public static class ViewModelLocator
    {

        // attached property
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        // contained within ViewModelLocator Class, default=Flase
        // Change to a Behavior: Wire-up a change event handler for the property
        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Guard Condition
            if (DesignerProperties.GetIsInDesignMode(d))
                return;
            // Get ViewType name, Assuming attached property will only be used on the root element
            var viewType = d.GetType();
            var viewTypeName = viewType.FullName;
            // from viewType we can infere viewModelType
            var viewModelTypeName = viewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);
            // Now we create an instance of that type using activator create instance method
            var viewModel = Activator.CreateInstance(viewModelType);
            // Finally, set it as datacontext of the view 
            ((FrameworkElement) d).DataContext = viewModel;
        }
    }
}
