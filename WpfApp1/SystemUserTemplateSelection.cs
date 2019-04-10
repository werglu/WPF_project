using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    public class SystemUserTemplateSelection : DataTemplateSelector
    {
       
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            Employee em = item as Employee;
            if(em.CompanyRole==Role.CEO)
            return
                       element.FindResource("Texttemp") as DataTemplate;

           else
                return
                       element.FindResource("Combotemp") as DataTemplate;
        }
    }
}
