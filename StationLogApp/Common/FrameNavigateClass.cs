using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StationLogApp.Common
{
    public class FrameNavigateClass
    {
        public void ActivateFrameNavigation(Type type)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(type);
            Window.Current.Content = frame;
            Window.Current.Activate();
        }

        public void ActivateFrameNavigation(Type type, Object obj)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(type, obj);
            Window.Current.Content = frame;
            Window.Current.Activate();
        }
    }
}
