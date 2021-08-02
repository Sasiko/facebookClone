using FacebookCloneApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FacebookCloneApp.Views
{
    /// <summary>
    /// Interaction logic for FindFriends.xaml
    /// </summary>
    public partial class FindFriends : Page
    {
        public FindFriends()
        {
            InitializeComponent();
            this.DataContext = new ProfileViewModel();
        }
    }
}
