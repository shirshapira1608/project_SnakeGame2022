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
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePageMode2 : Page
    {
        Manager manager;
        public User user = null;
        public GamePageMode2()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.user = DataBaseMethods.GetUser(user.UserName, user.Password);
            Frame.Navigate(typeof(MenuPage), this.user);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            manager = new Manager(arena, 2, user);
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

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Left)
                manager.Snake.Direction = Snake.DirectionType.left;
            if (args.VirtualKey == Windows.System.VirtualKey.Right)
                manager.Snake.Direction = Snake.DirectionType.right;
            if (args.VirtualKey == Windows.System.VirtualKey.Up)
                manager.Snake.Direction = Snake.DirectionType.up;
            if (args.VirtualKey == Windows.System.VirtualKey.Down)
                manager.Snake.Direction = Snake.DirectionType.down;
        }
    }
}
