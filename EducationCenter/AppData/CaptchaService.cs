using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EducationCenter.AppData
{
    public class CaptchaService
    {
        private const string _key = "012345";

        private List<ImageFragment> _imageFragments;

        public List<ImageFragment> CreateFragments(string[] imagesPath)
        {
            _imageFragments = new List<ImageFragment>();

            for (int i = 0; i < imagesPath.Length; i++)
            {
                ImageFragment imageFragment = new ImageFragment()
                {
                    Image = new BitmapImage(new Uri(imagesPath[i])),
                    Index = i,
                };
                _imageFragments.Add(imageFragment);
            }

            return _imageFragments;
        }

        public bool IsCorrect(ItemCollection itemes)
        {
            string result = string.Empty;

            foreach (var item in itemes)
            {
                if (item is ImageFragment imageFrafment)
                {
                    result += imageFrafment.Index.ToString();
                }
            }

            if (result == _key)
            {
                return true;
            }

            return false;
        }
    }
}
