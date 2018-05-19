using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.ServiceDiscovery.Dnssd;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using StationLogApp.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StationLogApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        private ButtonsVm Bvm { get; }
        
        public TaskPage()
        {
            this.InitializeComponent();
            this.DataContext = new VmContainer();
            CreateButonM.Visibility = Visibility.Collapsed;
            DeleteButtonM.Visibility = Visibility.Collapsed;
            UpdateButtonM.Visibility = Visibility.Collapsed;
            Bvm = new ButtonsVm();
            CheckIfManager();
        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage));
        }
        
        private void GoToNotesPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddNotesPage));
        }

        public void CheckIfManager()
        {
            if (Bvm.CurrentUser.GetUserType() != "manager" && Bvm.CurrentUser.GetUserType() != "admin") return;
            CreateButonM.Visibility = Visibility.Visible;
            DeleteButtonM.Visibility = Visibility.Visible;
            UpdateButtonM.Visibility = Visibility.Visible;
        }
    }
}