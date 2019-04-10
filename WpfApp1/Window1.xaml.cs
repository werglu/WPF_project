using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WpfApp1.MainWindow;


namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int salary;
        public int Salary {
            get { return salary; }
            set {
                salary = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Employee> list3 = new ObservableCollection<Employee>();
        public Window1( ObservableCollection<Employee> li)
        {
            InitializeComponent();
            date.SelectedDate= DateTime.Now.AddYears(-30);
            Salary = 5000;
            list3 = li;
         
        }
        

        private void Add_Employee(object sender, RoutedEventArgs e)
        {
            if (Salary< 5000)
                Salary = 5000;

            Employee em = new Employee(this.FirstName.Text, this.LastName.Text, "Male",(DateTime)date.SelectedDate, BirthCountry.Text, Salary, (Currency)(curr.SelectedIndex), (Role)(role.SelectedIndex));
            changes = true;
            if (r2.IsChecked.Value)
                em.Sex = "Female";
            
            list3.Add(em);
            FirstName.Text = "";
            LastName.Text = "";
            r1.IsChecked = true;
            r2.IsChecked = false;
            Salary=5000;
            BirthCountry.Text = "";
            curr.SelectedIndex = 0;
            role.SelectedIndex = 0;

            
        }
        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

       
    }
    
}
