using System;
using System.Collections.ObjectModel;
using FacebookCloneApp.Model;
using FacebookCloneApp.Views;
using MahApps.Metro.IconPacks;

namespace FacebookCloneApp.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
        private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> Menu => AppMenu;

        public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UserSolid },
                Label = "Profile",
                NavigationType = typeof(Profile),
                NavigationDestination = new Uri("Views/Profile.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.BloggerBrands },
                Label = "My Feeds",
                NavigationType = typeof(MyPosts),
                NavigationDestination = new Uri("Views/MyPosts.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UserFriendsSolid },
                Label = "Find Friends",
                NavigationType = typeof(FindFriends),
                NavigationDestination = new Uri("Views/FindFriends.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UserPlusSolid },
                Label = "Requests Received",
                NavigationType = typeof(FriendRequests),
                NavigationDestination = new Uri("Views/FriendRequests.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.UsersCogSolid },
                Label = "Friends List",
                NavigationType = typeof(MyFriends),
                NavigationDestination = new Uri("Views/MyFriends.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.BlogSolid },
                Label = "Feeds",
                NavigationType = typeof(PublicFeeds),
                NavigationDestination = new Uri("Views/PublicFeeds.xaml", UriKind.RelativeOrAbsolute)
            });
           

            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogsSolid },
                Label = "Settings",
                NavigationType = typeof(AboutPage),
                NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute)
            });
          
        }
    }
}