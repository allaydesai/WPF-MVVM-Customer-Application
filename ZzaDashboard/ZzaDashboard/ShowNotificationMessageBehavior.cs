using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ZzaDashboard
{
    // custom behavior that can show the alerts as they arrive in a content control
    public class ShowNotificationMessageBehavior : Behavior<ContentControl>
    {
        // expose a dependency property that can be set by the view model by data binding




        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        // Change handler to monitor change on this property and make the appropriate change in the view when it happens
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ShowNotificationMessageBehavior), new PropertyMetadata(OnMessageChanged));

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // d- class on which this dependency property is defined which is our Behavior
            var behavior = ((ShowNotificationMessageBehavior)d);
            // get to a property on our base class called AssociatedObject
            // set the content to the value of the message that was set
            behavior.AssociatedObject.Content = e.NewValue;
            // makes it pop open on the UI
            behavior.AssociatedObject.Visibility = Visibility.Visible;
        }
        // make notification click dismissible 
        // another handler, an override of the base class OnAttached method
        protected override void OnAttached()
        {
            // subscribe to mouse click event and  
            AssociatedObject.MouseLeftButtonDown += (s, e) => AssociatedObject.Visibility = Visibility.Collapsed;
        }
    }
}
