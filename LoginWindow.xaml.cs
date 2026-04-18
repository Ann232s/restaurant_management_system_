using System.Windows;
using System.Windows.Controls;
using RestaurantManagementSystem_2_.Models;

namespace RestaurantManagementSystem_2_
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LeftUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            LeftUserNameWatermark.Visibility =
                string.IsNullOrEmpty(LeftUserName.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void RightUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            RightUserNameWatermark.Visibility =
                string.IsNullOrEmpty(RightUserName.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = LeftUserName.Text.Trim();
            string password = LeftPasswordControl.Password.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите имя и пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = UserStore.Login(userName, password);

            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = RightUserName.Text.Trim();
            string password = RightPasswordControl.Password.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (UserStore.Register(userName, password))
            {
                MessageBox.Show("Регистрация успешна!", "Успех");
                var user = UserStore.GetUser(userName);
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже существует!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}