using DataBaseProject;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class StorePage : Page
    {
        public User user = null;
        public StorePage()
        {
            this.InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage), this.user);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter.ToString() != "")
            {
                this.user = (User)e.Parameter;
            }
        }
        private void DecorateUseButton(Button b)
        {
            b.Content = "use";
            b.BorderBrush = new SolidColorBrush(Colors.Red);
            b.Foreground = new SolidColorBrush(Colors.Red);
        }
        private void DecorateUsedButton(Button b)
        {
            b.Content = "used";
            b.BorderBrush = new SolidColorBrush(Colors.Blue);
            b.Foreground = new SolidColorBrush(Colors.Blue);
        }
        private void UpdateStore()
        {
            coins.Text = user.Coins.ToString();
            List<Purchases> purchases = DataBaseMethods.GetPurchases(this.user.Id);
            foreach (Purchases purchase in purchases)
            {
                switch (purchase.Product)
                {
                    case "background":
                        switch (purchase.ProductSerialNumber)
                        {
                            case 1:
                                DecorateUseButton(buyBackground1);
                                break;
                            case 2:
                                DecorateUseButton(buyBackground2);
                                break;
                            case 3:
                                DecorateUseButton(buyBackground3);
                                break;
                        }
                        break;
                    case "food":
                        switch (purchase.ProductSerialNumber)
                        {
                            case 1:
                                DecorateUseButton(buyBanana);
                                break;
                            case 2:
                                DecorateUseButton(buyCherry);
                                break;
                            case 3:
                                DecorateUseButton(buyStrawberry);
                                break;
                        }
                        break;
                    case "head":
                        switch (purchase.ProductSerialNumber)
                        {
                            case 1:
                                DecorateUseButton(buyPinkHead);
                                break;
                            case 2:
                                DecorateUseButton(buyYellowHead);
                                break;
                            case 3:
                                DecorateUseButton(buyBlueHead);
                                break;
                        }
                        break;
                }
            }
            switch (user.CurrentBackground)
            {
                case 0:
                    break;
                case 1:
                    DecorateUsedButton(buyBackground1);
                    break;
                case 2:
                    DecorateUsedButton(buyBackground2);
                    break;
                case 3:
                    DecorateUsedButton(buyBackground3);
                    break;
            }
            switch (user.CurrentFood)
            {
                case 0:
                    break;
                case 1:
                    DecorateUsedButton(buyBanana);
                    break;
                case 2:
                    DecorateUsedButton(buyCherry);
                    break;
                case 3:
                    DecorateUsedButton(buyStrawberry);
                    break;
            }
            switch (user.CurrentCharacter)
            {
                case 0:
                    break;
                case 1:
                    DecorateUsedButton(buyPinkHead);
                    break;
                case 2:
                    DecorateUsedButton(buyYellowHead);
                    break;
                case 3:
                    DecorateUsedButton(buyBlueHead);
                    break;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStore();
        }
        private void BuyBanana_Click(object sender, RoutedEventArgs e)
        {
            Purchases banana = new Purchases(user.Id, "food", 1);
            if (buyBanana.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 200, banana);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, banana);
            }
            UpdateStore();

        }
        private void BuyStrawberry_Click(object sender, RoutedEventArgs e)
        {
            Purchases strawberry = new Purchases(user.Id, "food", 3);
            if (buyStrawberry.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 300, strawberry);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, strawberry);
            }
            UpdateStore();
        }
        private void BuyCherry_Click(object sender, RoutedEventArgs e)
        {
            Purchases cherry = new Purchases(user.Id, "food", 2);
            if (buyCherry.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 250, cherry);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, cherry);
            }
            UpdateStore();
        }
        private void BuyBackground1_Click(object sender, RoutedEventArgs e)
        {
            Purchases background1 = new Purchases(user.Id, "background", 1);
            if (buyBackground1.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 200, background1);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, background1);
            }
            UpdateStore();
        }
        private void BuyBackground2_Click(object sender, RoutedEventArgs e)
        {
            Purchases background2 = new Purchases(user.Id, "background", 2);
            if (buyBackground2.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 250, background2);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, background2);
            }
            UpdateStore();
        }
        private void BuyBackground3_Click(object sender, RoutedEventArgs e)
        {
            Purchases background3 = new Purchases(user.Id, "background", 3);
            if (buyBackground3.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 300, background3);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, background3);
            }
            UpdateStore();
        }
        private void BuyYellowHead_Click(object sender, RoutedEventArgs e)
        {
            Purchases yellowHead = new Purchases(user.Id, "head", 2);
            if (buyYellowHead.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 250, yellowHead);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, yellowHead);
            }
            UpdateStore();
        }
        private void BuyBlueHead_Click(object sender, RoutedEventArgs e)
        {
            Purchases blueHead = new Purchases(user.Id, "head", 3);
            if (buyBlueHead.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 300, blueHead);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, blueHead);
            }
            UpdateStore();
        }
        private void BuyPinkHead_Click(object sender, RoutedEventArgs e)
        {
            Purchases pinkHead = new Purchases(user.Id, "head", 1);
            if (buyPinkHead.Content.Equals("buy"))
            {
                User check = DataBaseMethods.BuyProduct(user, 200, pinkHead);
                if (check == null)
                    CantBuy();
                else
                    this.user = check;
            }
            else
            {
                this.user = DataBaseMethods.UseProduct(user, pinkHead);
            }
            UpdateStore();
        }
        private async void CantBuy()
        {
            var dialog = new MessageDialog("You don't have enough money!");
            dialog.Title = "System notice";
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            await dialog.ShowAsync();
        }
    }
}
