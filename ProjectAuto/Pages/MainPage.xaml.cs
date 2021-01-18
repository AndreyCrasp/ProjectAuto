using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectAuto
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        AutoEntities context = new AutoEntities();
        public MainPage()
        {
            InitializeComponent();
            // List.ItemsSource = context.Product.ToList();
            var manufacturerlist = context.Manufacturer.ToList();
            manufacturerlist.Insert(0, new Manufacturer { Name = "Все элементы" });
            cmbManufactorer.ItemsSource = manufacturerlist;
            Update();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void cmbManufactorer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        void Update()
        {

            List<Product> currentList = context.Product.ToList();

            if (cmbManufactorer.SelectedIndex > 0)
            {
                currentList =
                    context.Product.ToList().Where(x => x.ManufacturerID ==
                    (cmbManufactorer.SelectedItem as Manufacturer).ID).ToList();
            }
            currentList = currentList.Where(x => x.Title.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            try
            {
                currentList = currentList.Where(x => x.Description.Contains(tbSearch.Text)).ToList();
            }
            catch { }
            rb1.IsChecked = false;
            rb2.IsChecked = false;
            tbCount.Text = $"Выведено: {currentList.Count()} из { context.Product.ToList().Count()}";
            List.ItemsSource = currentList;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var listcur = List.ItemsSource as List<Product>;
            List.ItemsSource = listcur.OrderByDescending(x => x.Cost).ToList();
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            var listcur = List.ItemsSource as List<Product>;
            List.ItemsSource = listcur.OrderBy(x => x.Cost).ToList();
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                Manager.MainFrame.Navigate(new InfoProductPage { DataContext = List.SelectedItem as Product });
                List.SelectedItem = null;
                Update();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddProductPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new HistoryPage());
        }
    }
}
