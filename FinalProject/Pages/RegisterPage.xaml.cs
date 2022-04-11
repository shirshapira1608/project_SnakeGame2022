using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class RegisterPage : Page
    {
        private User user;
        public RegisterPage()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(userName.Text != "" && password.Password != "" && confirmPassword.Password != "" && mail.Text != "")
            {
                if (password.Password.Equals(confirmPassword.Password))
                {
                    this.user = DataBaseMethods.AddUser(userName.Text, password.Password, mail.Text);
                    if (this.user != null)
                        Frame.Navigate(typeof(MenuPage), this.user);
                    else
                    {
                        var dialog = new MessageDialog("The user already exist. You have to identify|");
                        dialog.Title = "System notice";
                        dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    var dialog = new MessageDialog("Passwords are not the same!");
                    dialog.Title = "System notice";
                    dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                    await dialog.ShowAsync();
                }
            }
            else
            {
                var dialog = new MessageDialog("You have to fill every fields!");
                dialog.Title = "System notice";
                dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                await dialog.ShowAsync();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            userName.Text = "";
            password.Password = "";
            mail.Text = "";
            confirmPassword.Password = "";
        }
    }
}
