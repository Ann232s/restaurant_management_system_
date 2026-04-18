using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using RestaurantManagementSystem_2_.Models;
using RestaurantManagementSystem_2_.Pages;

namespace RestaurantManagementSystem_2_
{
    public partial class MainWindow : Window
    {
        private Border _selectedBorder = null;
        private User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            SetUserName(user.Username);
        }

        public void SetUserName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                UserNameTextBlock.Text = name;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetSelectedBorder(Border border)
        {
            if (_selectedBorder != null)
                _selectedBorder.Background = Brushes.Transparent;

            border.Background = new SolidColorBrush(Color.FromRgb(179, 179, 179));
            _selectedBorder = border;
        }

        private void ShowPlaceholder(bool show)
        {
            if (show)
            {
                PlaceholderText.Visibility = Visibility.Visible;
                MainFrame.Visibility = Visibility.Collapsed;
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Collapsed;
                MainFrame.Visibility = Visibility.Visible;
            }
        }

        private void SelectInvoices(object sender, MouseButtonEventArgs e)
        {
            SetSelectedBorder(InvoicesBorder);
            ShowPlaceholder(false);
            MainFrame.Navigate(new InvoicesPage(_currentUser));
        }

        private void SelectRecipes(object sender, MouseButtonEventArgs e)
        {
            SetSelectedBorder(RecipesBorder);
            ShowPlaceholder(false);
            MainFrame.Navigate(new RecipesPage(_currentUser));
        }

        private void SelectMenu(object sender, MouseButtonEventArgs e)
        {
            SetSelectedBorder(MenuBorder);
            ShowPlaceholder(false);
            MainFrame.Navigate(new MenuPage(_currentUser));
        }

        private void SelectSales(object sender, MouseButtonEventArgs e)
        {
            SetSelectedBorder(SalesBorder);
            ShowPlaceholder(false);
            MainFrame.Navigate(new SalesPage(_currentUser));
        }

        private void SelectEmployees(object sender, MouseButtonEventArgs e)
        {
            SetSelectedBorder(EmployeesBorder);
            ShowPlaceholder(false);
            MainFrame.Navigate(new EmployeesPage(_currentUser));
        }

        private void SelectAnalytics(object sender, MouseButtonEventArgs e)
        {
            SetSelectedBorder(AnalyticsBorder);
            ShowPlaceholder(false);
            MainFrame.Navigate(new AnalyticsPage(_currentUser));
        }
    }
}