using DataBaseProject;
using DataBaseProject.Models;
using FinalProject.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FinalProject
{
    public sealed partial class MenuPage : Page
    {
        public User user = null;
        public MenuPage()
        {
            this.InitializeComponent();
            this.user = null;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage), this.user);
        }

        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StorePage), this.user);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChooseMode), this.user);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.user == null)
                Frame.Navigate(typeof(LoginPage), this.user);
            else
                Frame.Navigate(typeof(LogoutPage), this.user);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ImageBrush menuBg = new ImageBrush();
            menuBg.ImageSource = new BitmapImage(new Uri(@"ms-appx:///Assets/pic/bg/backgroundMenuPage.png"));
            menuPage.Background = menuBg;
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
                this.playButton.IsEnabled = true;
                this.helpButton.IsEnabled = true;
                this.storeButton.IsEnabled = true;
                this.records.IsEnabled = true;
                this.changePasswordButton.Visibility = Visibility.Visible;
                Image logout = new Image();
                logout.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/pic/logos/logout.png"));
                this.loginButton.Content = logout;
            }
            else
            {
                this.user = null;
                this.playButton.IsEnabled = false;
                this.helpButton.IsEnabled = true;
                this.storeButton.IsEnabled = false;
                this.records.IsEnabled = false;
                this.changePasswordButton.Visibility = Visibility.Collapsed;
                Image login = new Image();
                login.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/pic/logos/login.png"));
                this.loginButton.Content = login;
            }
        }

        private void Records_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Records), this.user);
        }
        private async void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string userOldPass;
            string userNewPass;
            var oldPassBox = new TextBox
            {
                Width = 150,
                Height = 30,
                FontSize = 15
            };
            var newPassBox = new TextBox
            {
                Width = 150,
                Height = 30,
                FontSize = 15
            };
            var passBlock = new TextBlock
            {
                Width = 300,
                Height = 30,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.Red)
            };
            StackPanel panel = new StackPanel
            {
                Width = 200
            };
            panel.Orientation = Orientation.Vertical;
            panel.Children.Add(oldPassBox);
            panel.Children.Add(newPassBox);
            ContentDialog firstPopUp = new ContentDialog()
            {
                Title = "Enter your old pass and your new pass: ",
                Content = panel,
                Background = new SolidColorBrush(Colors.LightGray),
                Width = 400,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"
            };
            ContentDialog secondPopUp = new ContentDialog()
            {
                Title = "Your new password is: ",
                Content = passBlock,
                Background = new SolidColorBrush(Colors.LightGray),
                Width = 200,
                PrimaryButtonText = "Ok"
            };
            var answer = await firstPopUp.ShowAsync();
            if (answer == ContentDialogResult.Primary)
            {
                TextBox oldPassText = (TextBox)((StackPanel)firstPopUp.Content).Children[0];
                TextBox newPassText = (TextBox)((StackPanel)firstPopUp.Content).Children[1];
                userOldPass = oldPassBox.Text;
                userNewPass = newPassBox.Text;
                if (user.Password.Equals(userOldPass))
                {
                    this.user = DataBaseMethods.ChangePassword(user.UserName, userNewPass);
                    ((TextBlock)secondPopUp.Content).Text = user.Password;
                }
                else
                {
                    ((TextBlock)secondPopUp.Content).Text = "The data you entered is incorrect";
                    secondPopUp.Title = "Alert";
                }
                await secondPopUp.ShowAsync();
            }
        }
    }
}
