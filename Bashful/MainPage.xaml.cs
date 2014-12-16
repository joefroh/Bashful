using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HtmlAgilityPack;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Bashful.Resources;

namespace Bashful
{
    public partial class MainPage : PhoneApplicationPage
    {
        BashGrabber grabber = new BashGrabber();
        MainPageData data = new MainPageData();


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            data.CurrentQuote = "";
            this.DataContext = data;
            Button_Click(null, null);
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadingBar.Visibility = Visibility.Visible;
            var quote = await grabber.GetRandomQuotes();
            data.CurrentQuote = HttpUtility.HtmlDecode(quote.Value.QuoteText);
            data.Number = quote.Key;
            LoadingBar.Visibility = Visibility.Collapsed;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}