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
    public sealed partial class MenuPage : Page
    {
        private NavigationHelperVm Bvm { get; }
        private TaskVm Tvm { get; }

        public MenuPage()
        {
            this.InitializeComponent();
            this.DataContext = new VmContainer();
            MyFrame.Navigate(typeof(TaskPage));
            Bvm = new NavigationHelperVm();
            Tvm = new TaskVm();
        }

        private void TodoButton_OnClick(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(TaskPage));
        }

        private void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(TaskHistoryTechnicianPage));
        }

        private void MenuFlyoutItem_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage));
        }
    }
}
