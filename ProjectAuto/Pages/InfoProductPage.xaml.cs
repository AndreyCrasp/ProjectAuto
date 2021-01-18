using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для InfoProductPage.xaml
    /// </summary>
    public partial class InfoProductPage : Page
    {
        AutoEntities context = new AutoEntities();
        byte[] arr;
        ObservableCollection<ProductDOP> productDOPs = new ObservableCollection<ProductDOP>();
        public InfoProductPage()
        {
            InitializeComponent();
            
            cmbManufacturer.ItemsSource = context.Manufacturer.ToList();
            cmbProduct.ItemsSource = context.Product.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var prod = context.Product.Find((DataContext as Product).ID);
            prod.Title = tbTitle.Text;
            prod.Cost = Convert.ToInt32(tbCost.Text);
            prod.Description = tbDesk.Text;
            if (arr != null) { prod.Photo = arr; }
            
            prod.ManufacturerID = (cmbManufacturer.SelectedItem as Manufacturer).ID;
            prod.IsActive = Convert.ToBoolean(Check.IsChecked);
            

            context.SaveChanges();

            Manager.MainFrame.GoBack();
        }

        private void IMG_MouseDown(object sender, MouseButtonEventArgs e)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProduct.SelectedItem as Product == null) return;
            productDOPs.Add(new ProductDOP { MainProduct = (DataContext as Product).ID, AttachedProduct = (cmbProduct.SelectedItem as Product).ID });

            context.ProductDOP.AddRange(productDOPs.ToList());
            context.SaveChanges();
            list.ItemsSource = context.ProductDOP.ToList().Where(x => x.MainProduct == Convert.ToInt32(tbID.Text)).ToList();
            list.Items.Refresh();
        }

       

    private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = context.ProductDOP.ToList().Where(x => x.MainProduct == Convert.ToInt32(tbID.Text)).ToList();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(list.SelectedItem as ProductDOP!=null)
            context.ProductDOP.Remove(list.SelectedItem as ProductDOP);
            context.SaveChanges();
            list.ItemsSource = context.ProductDOP.ToList().Where(x => x.MainProduct == Convert.ToInt32(tbID.Text)).ToList();
            list.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить товар?","Предупреждение!",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes) { 
            context.Product.Remove(context.Product.Find((DataContext as Product).ID));
            context.SaveChanges();
            Manager.MainFrame.GoBack();
            }
        }
    }
}
