using DataBaseProject;
using DataBaseProject.Models;
using FinalProject.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FinalProject.Pages
{
    public sealed partial class GamePage : Page
    {
        public User user = null;
        Manager manager;
        public GamePage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.user = DataBaseMethods.GetUser(user.UserName, user.Password);
            Frame.Navigate(typeof(MenuPage), this.user);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            manager = new Manager(arena, 1, user);
            string uri = "";
            switch (user.CurrentBackground)
            {
                case 1:
                    uri = @"ms-appx:///Assets/pic/bg/background1.jpg";
                    break;
                case 2:
                    uri = @"ms-appx:///Assets/pic/bg/background2.jpg";
                    break;
                case 3:
                    uri = @"ms-appx:///Assets/pic/bg/background3.jpg";
                    break;
            }
            if (user.CurrentBackground != 0)
            {

                ImageBrush bg = new ImageBrush();
                bg.ImageSource = new BitmapImage(new Uri(uri));
                bg.Stretch = Stretch.Fill;
                arena.Background = bg;
            }
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
            }
        }
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if(args.VirtualKey == Windows.System.VirtualKey.Left)
                manager.Snake.Direction = Snake.DirectionType.left;
            if(args.VirtualKey == Windows.System.VirtualKey.Right)
                manager.Snake.Direction = Snake.DirectionType.right;
            if(args.VirtualKey == Windows.System.VirtualKey.Up)
                manager.Snake.Direction = Snake.DirectionType.up;
            if(args.VirtualKey == Windows.System.VirtualKey.Down)
                manager.Snake.Direction = Snake.DirectionType.down;
        }
    }
}
