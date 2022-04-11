using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using static FinalProject.Classes.Snake;

namespace FinalProject.Classes
{
    partial class Food
    {
        private double placeX;
        private double placeY;
        private Random random;
        private double size;
        private Canvas arena;
        public int Point { get; }
        public Image Image { get; set; }
        public Food(Canvas arena, int point, string uri, double size)
        {
            this.arena = arena;
            random = new Random();
            this.Image = new Image();
            this.placeX = random.Next(0, (int)this.arena.ActualWidth-(int)size);
            this.placeY = random.Next(0, (int)this.arena.ActualHeight-(int)size);
            this.size = size;
            this.Image.Width = this.size;
            this.Image.Height = this.size;
            this.Point = point;
            this.Image.Source = new BitmapImage(new Uri(uri));
            arena.Children.Add(Image);
            Canvas.SetLeft(Image, this.placeX);
            Canvas.SetTop(Image, this.placeY);
        }
        public void Replace()
        {
            try
            {
                this.placeX = random.Next(0, (int)this.arena.ActualWidth - (int)size);
                this.placeY = random.Next(0, (int)this.arena.ActualHeight - (int)size);
                Canvas.SetLeft(Image, this.placeX);
                Canvas.SetTop(Image, this.placeY);
            }
            catch
            {

            }
        }

    }
}
