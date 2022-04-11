using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Records : Page
    {
        private User user = null;
        public Records()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> topUsers = DataBaseMethods.RecordTable(user);
            user1.Text = topUsers[1].UserName;
            user2.Text = topUsers[2].UserName;
            user3.Text = topUsers[3].UserName;
            you.Text = topUsers[0].UserName;
            rec1.Text = topUsers[1].MaxScore.ToString();
            rec2.Text = topUsers[2].MaxScore.ToString();
            rec3.Text = topUsers[3].MaxScore.ToString();
            yourec.Text = topUsers[0].MaxScore.ToString();
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
            Frame.Navigate(typeof(MenuPage), this.user);
        }
    }
}
