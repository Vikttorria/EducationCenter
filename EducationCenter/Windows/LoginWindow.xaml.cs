using EducationCenter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducationCenter.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        EducationCenterContext context= new EducationCenterContext();
        private int failedAttempCount;

        public LoginWindow()
        {
            InitializeComponent();

            LoginTb.Text = Properties.Settings.Default.UserLogin;
            PasswordPb.Password = Properties.Settings.Default.UserPassword;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения. Заполните поле \"Логин\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MessageBox.Show("Все поля обязательны для заполнения. Заполните поле \"Пароль\"", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User? user = context.Users.FirstOrDefault(u=> u.Login == LoginTb.Text && u.Password == PasswordPb.Password);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден. Проверьте правильность логина и пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                failedAttempCount++;

                return;
            }
            else
            {
                if(user.Employee.IsActive == false)
                {
                    MessageBox.Show("Вы заблокированны. Обратитесь к координатору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                CaptchaWindow captchaWindow = new CaptchaWindow();
                if(captchaWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Вы успешно авторизовались", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                    if(RememberMeCb.IsChecked == true)
                    {
                        Properties.Settings.Default.UserLogin = LoginTb.Text;
                        Properties.Settings.Default.UserPassword = PasswordPb.Password;
                    }
                    else
                    {
                        Properties.Settings.Default.UserLogin = "";
                        Properties.Settings.Default.UserPassword = "";

                    }
                    Properties.Settings.Default.Save();

                    if (user.RoleId == 5)
                    {
                        CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
                        coordinatorWindow.Show();

                        Close();   
                    }
                }
                else
                { 
                    MessageBox.Show("Капча собрана неверно. Попробуйте еще раз.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    failedAttempCount++;
                    if(failedAttempCount >= 3)
                    {
                        user.Employee.IsActive = false;
                        context.SaveChanges();

                        MessageBox.Show("Вы заблокированны. Обратитесь к координатору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    return;
                }
            }

            //if (LoginTb.Text == "ivanova" && PasswordPb.Password == "123" || LoginTb.Text == "petrov" && PasswordPb.Password == "234")
            //{
            //    MessageBox.Show("Вы успешно авторизовались"); 
            //    Captcha captcha = new Captcha();
            //    Close();


            //    if (captcha.ShowDialog() == true)
            //    {

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные");
            //}
        }
    }
}
