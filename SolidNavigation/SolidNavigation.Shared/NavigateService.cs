using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SolidNavigation.Sdk;

namespace SolidNavigation
{
    public class NavigateService:NavigationServiceBase
    {
        private static NavigateService _current;
        public static NavigateService Current { get { return _current ?? (_current = new NavigateService()); } }

        public MasterView MasterView
        {
            get { return (MasterView)Window.Current.Content; }
        }

        public Frame ContentFrame
        {
            get { return MasterView.FindName("ContentFrame") as Frame; }
        }

        public void GoBack()
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        protected override void Navigate(Route route, NavigationTarget target, string uri)
        {
            ContentFrame.Navigate(route.PageType, uri);
        }
    }
}
    