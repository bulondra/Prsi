using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Prsi
{
    public class ComponentManager
    {
        private MainWindow instance;
        public ComponentManager(MainWindow instance)
        {
            this.instance = instance;
        }
        
        // Image creation
        public Image createImage(string name, string path, int angle, Thickness margin)
        {
            Image img = new Image(); // Create new image
            img.Width = 180; // Set width
            img.Height = 300;; // Set height
            img.Margin = margin; // Set margin (left, top, right, bottom)
            ImageSource imgSrc = new BitmapImage(new Uri(path)); // Init source
            img.Source = imgSrc; // Set source to image
            img.Name = name; // Set  name
            
            RotateTransform rotateTransform = new RotateTransform(angle) { CenterX = img.ActualHeight / 2, CenterY = img.ActualWidth / 2 }; // Init angle of image
            img.LayoutTransform = new RotateTransform(angle); // Set angle of image
            
            return img; // Return image
        }

        // Selector image creation
        public Image createSelectorImage(string name, string path, Thickness margin)
        {
            Image img = new Image(); // Create new image
            img.Width = 100; // Set width
            img.Height = 100;; // Set height
            img.Margin = margin; // Set margin (left, top, right, bottom)
            ImageSource imgSrc = new BitmapImage(new Uri(path)); // Init source
            img.Source = imgSrc; // Set source to image
            img.Name = name; // Set  name

            return img; // Return image
        }
    }
}