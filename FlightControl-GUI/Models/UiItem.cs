using CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FlightControl_GUI.Models
{
    public class UiItem
    {
        string _image { get; set; }
        double _height { get; set; }
        double _width { get; set; }
        double _x { get; set; }
        double _y { get; set; }
        public Image Image { get; set; }
        public Plane? Plane { get; set; }
        public int Step { get; set; }

        public UiItem(double height, double width, double x, double y, string image)
        {
            _height = height;
            _width = width;
            _x = x;
            _y = y;
            _image = image;
            Image = SetImage();
            Canvas.SetLeft(Image, _x);
            Canvas.SetTop(Image, _y);
            Step = 10;
        }

        public Image SetImage()
        {
            Image _img = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(_image, UriKind.Absolute);
            bitmap.EndInit();
            _img.Source = bitmap;
            _img.Height = _height;
            _img.Width = _width;
            return _img;
        }

        public double ImgLeft()
        {
            return Canvas.GetLeft(Image);
        }
        public double ImgTop()
        {
            return Canvas.GetTop(Image);
        }
    }
}
