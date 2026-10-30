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
    /// Логика взаимодействия для CoordinatorWindow.xaml
    /// </summary>
    public partial class CoordinatorWindow : Window
    {
        EducationCenterContext context = new EducationCenterContext();
        public CoordinatorWindow()
        {
            InitializeComponent();

            UserLv.ItemsSource = new List<string>();
        }

        private void StartFilterSearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
