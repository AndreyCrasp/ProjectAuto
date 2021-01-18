using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        AutoEntities context = new AutoEntities();
        byte[] arr;
        public AddProductPage()
        {
            InitializeComponent();
            cmbManufacturer.ItemsSource = context.Manufacturer.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var prod = new Product();
            prod.Title = tbTitle.Text;
            prod.Cost = Convert.ToInt32(tbCost.Text);
            prod.Description = tbDesk.Text;
            prod.Photo = arr;
            prod.ManufacturerID = (cmbManufacturer.SelectedItem as Manufacturer).ID;
            prod.IsActive = Convert.ToBoolean(Check.IsChecked);
            context.Product.Add(prod);
            
            context.SaveChanges();
            
            Manager.MainFrame.GoBack();
        }

        private void IMG_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IMG|*.jpg;*.png;*jpeg;*.bmp";
            if (openFileDialog.ShowDialog()==true)
            {
                arr = File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage bmp = new BitmapImage(new Uri(openFileDialog.FileName));
                IMG.Source = bmp;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IMG|*.jpg;*.png;*jpeg;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                arr = File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage bmp = new BitmapImage(new Uri(openFileDialog.FileName));
                IMG.Source = bmp;
            }
        }
    }
}
