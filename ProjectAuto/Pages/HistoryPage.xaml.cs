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

namespace ProjectAuto
{
    /// <summary>
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        AutoEntities context = new AutoEntities();
        public HistoryPage()
        {
            InitializeComponent();
            datagrid.ItemsSource = context.ProductSale.OrderByDescending(x => x.SaleDate).ToList();
            var correntlist = context.Product.ToList();
            correntlist.Insert(0, new Product { Title = "Все" });
            cmbProduct.ItemsSource = correntlist;
            cmbProduct.SelectedIndex = 0;
        }

        private void cmbmanufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentlist = context.ProductSale.OrderByDescending(x => x.SaleDate).ToList();
            if (cmbProduct.SelectedIndex > 0)
            {
                currentlist = currentlist.Where(x => x.ProductID == (cmbProduct.SelectedItem as Product).ID).ToList();
            }
            datagrid.ItemsSource = currentlist;
        }
    }
}
