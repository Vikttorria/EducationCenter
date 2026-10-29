using EducationCenter.AppData;
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
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        CaptchaService captchaService;
        string[] imagePath;


        public Captcha()
        {
            InitializeComponent();

            captchaService = new CaptchaService();
            imagePath = new string[]
            {
               "C:\\Users\\0dmin\\source\\repos\\34\\EducationCenter\\EducationCenter\\Image\\1.png",

               "C:\\Users\\0dmin\\source\\repos\\34\\EducationCenter\\EducationCenter\\Image\\2.png",

               "C:\\Users\\0dmin\\source\\repos\\34\\EducationCenter\\EducationCenter\\Image\\3.png",

               "C:\\Users\\0dmin\\source\\repos\\34\\EducationCenter\\EducationCenter\\Image\\4.png",

               "C:\\Users\\0dmin\\source\\repos\\34\\EducationCenter\\EducationCenter\\Image\\5.png",

               "C:\\Users\\0dmin\\source\\repos\\34\\EducationCenter\\EducationCenter\\Image\\6.png",

            };
            foreach (var imageFragment in captchaService.CreateFragments(imagePath))
            {
                ImageFrafment.Items.Add(imageFragment);
            }
        }
        private void CaptchaLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CaptchaLb.SelectedItem is ImageFragment imageFrafment)
            {
                ImageFrafment.Items.Add(imageFrafment);
                CaptchaLb.Items.Remove(imageFrafment);
            }
        }



        private void ImageFrafment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ImageFrafment.SelectedItem is ImageFragment imageFrafment)
            {
                CaptchaLb.Items.Add(imageFrafment);
                ImageFrafment.Items.Remove(imageFrafment);

                if (CaptchaLb.Items.Count == 6)
                {
                    if (captchaService.IsCorrect(CaptchaLb.Items))
                    {

                        CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
                        coordinatorWindow.Show();

                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Вы -робот!");
                    }
                }
            }


        }
    }
}
