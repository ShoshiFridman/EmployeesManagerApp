using BL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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


namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Interviews interviews;
        List<string> categories;
        private bool isUpdating = false;

        public MainWindow()
        {
            interviews = new Interviews();

            InitializeComponent();

            var employees = interviews.GetEmployees();
            CandidateDataGrid.ItemsSource = employees;

            var filter = interviews.GetRoles();
           ComboBoxFilterCategory.ItemsSource= filter;

            categories = new List<string>() { "Role", "City", "Starting working year", "Decade" };
            ComboBoxFilterOptions.ItemsSource = categories;
            ComboBoxFilterOptions.SelectionChanged += ComboBoxFilterOptions_SelectionChanged;
            ComboBoxFilterCategory.SelectionChanged += ComboBoxFilterCategory_SelectionChanged;
        }


        public bool Refresh(Employee employee)
        {
            var employees = interviews.GetEmployees();
            CandidateDataGrid.ItemsSource = employees;

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.EventAddEmployee += Refresh; // שימוש בארוע הוספת עובד
            addEmployee.ShowDialog();
        }

        private void CandidateDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdating || ComboBoxFilterOptions.SelectedItem == null || ComboBoxFilterCategory.SelectedItem == null)
                return;
            if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[0])
            {
                var employeesByRole = interviews.GetEmployeesByRole(ComboBoxFilterCategory.SelectedItem.ToString());
                CandidateDataGrid.ItemsSource = employeesByRole;
            }
            else
            {
                if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[1])
                {
                    var employeesByCity = interviews.GetEmployeesByCity(ComboBoxFilterCategory.SelectedItem.ToString());
                    CandidateDataGrid.ItemsSource = employeesByCity;
                }
                else
                {
                    if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[2])
                    {
                        var employeesByStartWork = interviews.GetEmployeesByStartWork(int.Parse(ComboBoxFilterCategory.SelectedItem.ToString()));
                        CandidateDataGrid.ItemsSource = employeesByStartWork;
                    }
                    else
                    {
                        if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[3])
                        {
                            var employeesByDecade = interviews.GetEmployeesByDecade(ComboBoxFilterCategory.SelectedItem.ToString());
                            CandidateDataGrid.ItemsSource = employeesByDecade;
                        }
                    }
                }
            }
        }

        private void ComboBoxFilterOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isUpdating = true;
            ComboBoxFilterCategory.ItemsSource = null;
            if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[0])
            {
                var filter = interviews.GetRoles();
                ComboBoxFilterCategory.ItemsSource = filter;
            }
            else
            {
                if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[1])
                {
                    var filter = interviews.GetCity();
                    ComboBoxFilterCategory.ItemsSource = filter;
                }
                else
                {
                    if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[2])
                    {
                        var filter = interviews.GetStartWork();
                        ComboBoxFilterCategory.ItemsSource = filter;
                    }
                    else
                    {
                        if (ComboBoxFilterOptions.SelectedItem.ToString() == categories[3])
                        {
                            var filter = interviews.GetAgeDecade();
                            ComboBoxFilterCategory.ItemsSource = filter;
                        }
                    }
                }
            }
            var employees = interviews.GetEmployees();
            CandidateDataGrid.ItemsSource = employees;
            isUpdating = false;
        }
    }
}
