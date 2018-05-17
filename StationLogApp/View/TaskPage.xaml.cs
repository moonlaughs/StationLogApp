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
using StationLogApp.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StationLogApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TaskPage : Page
    {
        public TaskPage()
        {
            this.InitializeComponent();
            this.DataContext = typeof(VMContainer);
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TaskHistoryTechnicianPage));
        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage));
        }


        private void GoToNotesPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddNotesPage));
        }
    }
}
