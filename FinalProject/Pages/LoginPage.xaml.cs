using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private User user;

        public object DataBaseMethod { get; private set; }

        public LoginPage()
        {
            this.InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage), this.user);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.user = DataBaseMethods.GetUser(userName.Text, password.Password);
            if (this.user == null)
            {
                var dialog = new MessageDialog("User doesn't exsist");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "OK", Id = 0 });
                dialog.ShowAsync();
            }
            else
                Frame.Navigate(typeof(MenuPage), this.user);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            userName.Text = "";
            password.Password = "";
        }

        private async void ForgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string userName;
            string userMail;
            var nameBox = new TextBox
            {
                Width = 150,
                Height = 30,
                FontSize = 15
            };
            var mailBox = new TextBox
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
            panel.Children.Add(nameBox);
            panel.Children.Add(mailBox);
            ContentDialog firstPopUp = new ContentDialog()
            {
                Title = "Enter your userName and your mail: ",
                Content = panel,
                Background = new SolidColorBrush(Colors.LightGray),
                Width = 400,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"
            };
            ContentDialog secondPopUp = new ContentDialog()
            {
                Title = "Your password is: ",
                Content = passBlock,
                Background = new SolidColorBrush(Colors.LightGray),
                Width = 200,
                PrimaryButtonText = "Ok"
            };
            var answer = await firstPopUp.ShowAsync();
            if(answer == ContentDialogResult.Primary)
            {
                TextBox nameText = (TextBox)((StackPanel)firstPopUp.Content).Children[0];
                TextBox mailText = (TextBox)((StackPanel)firstPopUp.Content).Children[1];
                userName = nameText.Text;
                userMail = mailText.Text;
                User user = DataBaseMethods.ForgotPassword(userName, userMail);
                if (user != null)
                    ((TextBlock)secondPopUp.Content).Text = user.Password;
                else
                    ((TextBlock)secondPopUp.Content).Text = "The data you entered is incorrect";
                await secondPopUp.ShowAsync();
            }
        }
    }
}
