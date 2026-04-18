using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RestaurantManagementSystem_2_.Helpers;
using RestaurantManagementSystem_2_.Models;

namespace RestaurantManagementSystem_2_.Pages
{
    public partial class AddInvoicePage : Page
    {
        private ObservableCollection<InvoiceProduct> products = new ObservableCollection<InvoiceProduct>();
        private User _currentUser;

        public AddInvoicePage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            ProductsGrid.ItemsSource = products;
            products.Add(new InvoiceProduct { Number = 1 });
        }

        private void ProductsGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    for (int i = 0; i < products.Count; i++)
                    {
                        products[i].Number = i + 1;
                    }

                    decimal total = products.Sum(p => p.Quantity * p.Price);
                    TotalAmountText.Text = total.ToString("F2");
                }
                catch { }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void ProductsGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            e.NewItem = new InvoiceProduct { Number = products.Count + 1 };
        }

        private void SaveInvoice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string number = InvoiceNumberBox.Text?.Trim();
                string date = InvoiceDateBox.Text?.Trim();
                string supplier = InvoiceSupplierBox.Text?.Trim();

                string category = "Продукты";
                if (CategoryCombo.SelectedItem is ComboBoxItem selectedItem)
                {
                    category = selectedItem.Content.ToString();
                }

                if (string.IsNullOrEmpty(number) || string.IsNullOrEmpty(date) ||
                    string.IsNullOrEmpty(supplier))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var validProducts = products.Where(p => !string.IsNullOrEmpty(p.Name)).ToList();
                if (validProducts.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы один товар!", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newInvoice = new InvoiceCard
                {
                    Number = number,
                    Date = date,
                    Supplier = supplier,
                    Category = category,
                    Products = validProducts
                };

                _currentUser.Invoices.Add(newInvoice);

                FileHelper.SaveUsers();

                MessageBox.Show($"Накладная {number} сохранена!\n" +
                              $"Товаров: {validProducts.Count}",
                              "Успех",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information);

                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
