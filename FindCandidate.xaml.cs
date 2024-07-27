using BL;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for FindCandidate.xaml
    /// </summary>
    public partial class FindCandidate : Window
    {
        Interviews interviews;
        public FindCandidate()
        {
            interviews = new Interviews();

            InitializeComponent();

            var candidate = interviews.GetNameCandidencies();
            ComboboxChooseCandidate.ItemsSource = candidate;
        }

        private void ComboboxChooseCandidate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var interview = interviews.GetInterviewsDetails(ComboboxChooseCandidate.SelectedItem.ToString());
            DataGridInterviews.ItemsSource = interview;
        }

        private void DataGridInterviews_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboboxChooseCandidate_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var interview = interviews.GetInterviewsDetails(ComboboxChooseCandidate.SelectedItem.ToString());
            DataGridInterviews.ItemsSource = interview;

        }
    }
}