using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
 
   
    public enum Currency { PLN, USD, EUR, GBP, NOK }
    public enum Role { Worker, SeniorWorker, IT, Manager, Director, CEO }

    public partial class MainWindow : Window
    {
        public string fname { get; set; }

        public ObservableCollection<Employee> list = new ObservableCollection<Employee>();
        public static bool changes = false;
        public static void  ch()
        {
            changes = true;
        }

        public class Employee : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Sex { get; set; }
            public DateTime BirthDate { get; set; }
            public string BirthCountry { get; set; }

            private int salary=5000;
            public int Salary
            {
                get
                {
                    return this.salary;
                }
                set
                {
                    if (this.salary != value)
                    {
                       // ch();
                        this.salary = value;
                        this.NotifyPropertyChanged("Salary");
                    }
                }
            }
            private Currency salaryCurrency;
            public Currency SalaryCurrency
            {
                get
                {
                    return this.salaryCurrency;
                }
                set
                {
                    if (this.salaryCurrency != value)
                    {
                       // ch();
                        this.salaryCurrency = value;
                        this.NotifyPropertyChanged("CompanyRole");
                    }
                }
            }
            private Role companyRole;
            public Role CompanyRole
            {
                get
                {
                    return this.companyRole;
                }
                set
                {
                    if (this.companyRole != value)
                    {
                        //ch();
                        this.companyRole = value;
                        this.NotifyPropertyChanged("CompanyRole");
                    }
                }
            }

            public Employee(string firstName, string lastName, string sex, DateTime birthDate, string birthCountry, int salary, Currency salaryCurrency, Role companyRole)
            {
                FirstName = firstName;
                LastName = lastName;
                Sex = sex;
                BirthDate = birthDate;
                BirthCountry = birthCountry;
                Salary = salary;
                SalaryCurrency = salaryCurrency;
                CompanyRole = companyRole;
                changes = false;
            }

            public void NotifyPropertyChanged(string propName)
            {

                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                changes = true;
            }

        }

        public MainWindow()
        {
            InitializeComponent();

          
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (changes == false )
            {//open and dont save
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                
                openFileDialog.ShowDialog();
                
                string fileName = openFileDialog.FileName;
                if (fileName.Length!=0)
                {
                    list = new ObservableCollection<Employee>();
                    ReadCSV(openFileDialog.FileName);

                    l.ItemsSource = list;
                    fname = openFileDialog.FileName;
                }
                
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes before opening new file?", "Save changes", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {//save
                            string[] line = new string[list.Count + 1];
                            line[0] = "First Name;Last Name;Sex;Birth Date;Birth Country;Salary;SalaryCurrency;CompanyRole";
                            int i = 1;
                            foreach (var em in list)
                            {
                                line[i] = em.FirstName + ";" + em.LastName + ";" + em.Sex + ";" + em.BirthDate.Day.ToString() + "." + em.BirthDate.Month.ToString() + "." + em.BirthDate.Year.ToString() + ";" + em.BirthCountry + ";" + em.Salary.ToString() + ";" + ((int)em.SalaryCurrency).ToString() + ";" + ((int)em.CompanyRole).ToString();
                                i++;
                            }
                            if (File.Exists(fname))
                                File.Delete(fname);

                            File.WriteAllLines(fname, line);
                            ///open
                            OpenFileDialog openFileDialog = new OpenFileDialog();

                            openFileDialog.Filter = "csv files (*.csv)|*.csv";


                            openFileDialog.ShowDialog();


                            string fileName = openFileDialog.FileName;
                            list = new ObservableCollection<Employee>();
                            ReadCSV(openFileDialog.FileName);

                            l.ItemsSource = list;
                            fname = openFileDialog.FileName;
                            changes = false;
                        }
                        
                        break;
                    case MessageBoxResult.No:
                        {//open
                            OpenFileDialog openFileDialog = new OpenFileDialog();

                            openFileDialog.Filter = "csv files (*.csv)|*.csv";


                            openFileDialog.ShowDialog();


                            string fileName = openFileDialog.FileName;
                            list = new ObservableCollection<Employee>();
                            ReadCSV(openFileDialog.FileName);

                            l.ItemsSource = list;
                            fname = openFileDialog.FileName;
                            changes = false;
                        }
                        break;
                    case MessageBoxResult.Cancel:
                        {
                           
                            changes = false;
                        }
                        break;
                }
            }

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0) return;

            string[] line = new string[list.Count + 1];
            line[0] = "First Name;Last Name;Sex;Birth Date;Birth Country;Salary;SalaryCurrency;CompanyRole";
            int i = 1;
            foreach (var em in list)
            {

                line[i] = em.FirstName + ";" + em.LastName + ";" + em.Sex + ";" + em.BirthDate.Day.ToString() + "." + em.BirthDate.Month.ToString() + "." + em.BirthDate.Year.ToString() + ";" + em.BirthCountry + ";" + em.Salary.ToString() + ";" + ((int)em.SalaryCurrency).ToString() + ";" + ((int)em.CompanyRole).ToString();
                i++;

            }
            if (File.Exists(fname))
            {
                File.Delete(fname);
            }
            File.WriteAllLines(fname, line);
             changes = false;

        }
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0) return;
            SaveFileDialog openFileDialog = new SaveFileDialog();

            openFileDialog.Filter = "csv files (*.csv)|*.csv";
          
           
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length == 0) return;
            string[] line = new string[list.Count+1];
            line[0] = "First Name;Last Name;Sex;Birth Date;Birth Country;Salary;SalaryCurrency;CompanyRole";
            int i = 1;
            foreach (var em in list)
            {
               
                line[i] = em.FirstName + ";" + em.LastName + ";" + em.Sex + ";" + em.BirthDate.Day.ToString() + "." + em.BirthDate.Month.ToString() + "." + em.BirthDate.Year.ToString() + ";" + em.BirthCountry + ";" +em.Salary.ToString()+ ";" + ((int)em.SalaryCurrency).ToString() + ";" + ((int)em.CompanyRole).ToString();
                i++;
               
            }
            if (File.Exists(openFileDialog.FileName))
            {
                File.Delete(openFileDialog.FileName);
            }
            File.WriteAllLines(openFileDialog.FileName, line);

            fname = openFileDialog.FileName;
            changes = false;


        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {if (changes == true)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save changes before closing?", "Save changes", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            if (list.Count == 0) return;
                            SaveFileDialog openFileDialog = new SaveFileDialog();

                            openFileDialog.Filter = "csv files (*.csv)|*.csv";


                            openFileDialog.ShowDialog();
                            if (openFileDialog.FileName.Length == 0) return;

                            string[] line = new string[list.Count + 1];
                            line[0] = "First Name;Last Name;Sex;Birth Date;Birth Country;Salary;SalaryCurrency;CompanyRole";
                            int i = 1;
                            foreach (var em in list)
                            {
                                line[i] = em.FirstName + ";" + em.LastName + ";" + em.Sex + ";" + em.BirthDate.Day.ToString() + "." + em.BirthDate.Month.ToString() + "." + em.BirthDate.Year.ToString() + ";" + em.BirthCountry + ";" + em.Salary.ToString() + ";" + ((int)em.SalaryCurrency).ToString() + ";" + ((int)em.CompanyRole).ToString();
                                i++;
                            }
                            if (File.Exists(openFileDialog.FileName))
                                File.Delete(openFileDialog.FileName);

                            File.WriteAllLines(openFileDialog.FileName, line);

                        }
                        Environment.Exit(0);
                        break;
                    case MessageBoxResult.No:
                        Environment.Exit(0);
                        break;
                    case MessageBoxResult.Cancel:

                        break;
                }
            }
        else
            Environment.Exit(0);

        }
        public void ReadCSV(string fileName)
        {
            StreamReader f = new StreamReader(fileName);

         
            f.ReadLine();
            while (!f.EndOfStream)
            {
                var line = f.ReadLine();
                var data = line.Split(';');
                string[] d = data[3].Split('.');

               list.Add(new Employee(data[0], data[1], data[2], new DateTime(Int32.Parse(d[2]), Int32.Parse(d[1]), Int32.Parse(d[0])), data[4], Int32.Parse(data[5]), (Currency)Int32.Parse(data[6]), (Role)Int32.Parse(data[7])));
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            int index = this.l.SelectedIndex;
            if(index>=0 && index+1<this.list.Count)
            {
                var it = this.list[index];
                this.list.RemoveAt(index);
                this.list.Insert(index + 1, it);
                this.l.SelectedIndex = index + 1;
                changes = true;

            }
           

        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            int index = this.l.SelectedIndex;
            if (index > 0)
            {
                var it = this.list[index];
                this.list.RemoveAt(index);
                this.list.Insert(index - 1, it);
                this.l.SelectedIndex = index - 1;
                changes = true;
            }

        }
        private Window1 win = null;
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0) return;
            if (win == null)
            {
                win = new Window1( list);
                
                win.Closed += WindowClosed;
              
                win.FontSize = 14;
                win.Topmost = true;
                double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                win.Width = 500;
                win.Height = 500;
                win.ResizeMode = ResizeMode.CanMinimize;
              

                win.Left = (screenWidth / 2) - 250;
                win.Top = (screenHeight / 2) - 250;
               
                win.Show();
                
            }
            
            else if (win.WindowState == WindowState.Minimized)
                win.WindowState=WindowState.Normal;
               
            
        }
        public void WindowClosed(object sender, System.EventArgs e)
        {
           win = null;
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (l.SelectedIndex >= 0)
            {
                list.RemoveAt(l.SelectedIndex);
                changes = true;
            }

        }
     
    }

  
}
