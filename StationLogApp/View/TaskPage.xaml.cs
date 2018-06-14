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
        private NavigationHelperVm Bvm { get; }
        //private TaskVm Tvm { get; }
        //private NoteVm Nvm { get; }
        //private CreateTaskVm Ctvm { get; }
        //private UpdateTaskVm Uvm { get; }

        public TaskPage()
        {
            this.InitializeComponent();
            this.DataContext = new VmContainer();
            CreateButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Collapsed;
            Bvm = new NavigationHelperVm();
            //Tvm = new TaskVm();
            //Nvm = new NoteVm();
            //Ctvm = new CreateTaskVm();
            //Uvm = new UpdateTaskVm();
            CheckIfManager();
        }

        public void CheckIfManager()
        {
            if (Bvm.CurrentUser.GetUserType() != "manager" && Bvm.CurrentUser.GetUserType() != "admin") return;
            CreateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
        }
    }
}