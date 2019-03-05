using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZzaDashboard
{
    public static class MVVM_Behaviors
    {


        // behavior through a custom attach property
        // used to communicate between view and view model
        public static string GetLoadedMethodName(DependencyObject obj)
        {
            return (string)obj.GetValue(LoadedMethodNameProperty);
        }

        public static void SetLoadedMethodName(DependencyObject obj, string value)
        {
            obj.SetValue(LoadedMethodNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for LoadedMethodName.  This enables animation, styling, binding, etc...
        // OnLoadedMethodNameChanged is a change handler
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached("LoadedMethodName", typeof(string), typeof(MVVM_Behaviors), new PropertyMetadata(null, OnLoadedMethodNameChanged));

        private static void OnLoadedMethodNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // d- object on which attached property was set

            // Cast to Framework element 
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                // handle view loaded event here
                element.Loaded += (s, e2) =>
                {
                    var viewModel = element.DataContext;
                    if (viewModel == null)
                        return;
                    // use reflection to get a reference to the method of the name that is being set for LoadedMethodName 
                    var methodInfo = viewModel.GetType().GetMethod(e.NewValue.ToString());
                    if (methodInfo != null)
                        // Once we have that reference, we can just invoke it through reflection
                        methodInfo.Invoke(viewModel, null);
                };


            }
        }
    }
}
