using Windows.System;
using Windows.UI.Xaml;
using SolidNavigation.Sdk;

namespace SolidNavigation {
    public sealed partial class MasterView {
        public MasterView() {
            InitializeComponent();
            AddressBox.KeyDown += AddressBox_KeyDown;
        }

        void AddressBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e) {
            if (e.Key == VirtualKey.Enter) {
                NavigateService.Current.Navigate(AddressBox.Text);
            }
        }

        public void ShowTarget(string url) {
            var target = Router.Current.CreateTarget(url);
            NavInfo.Text = url + "\n" + target;
        }

        private void OnGoClick(object sender, RoutedEventArgs e) {
            NavigateService.Current.Navigate(AddressBox.Text);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e) {
            NavigateService.Current.GoBack();
        }
    }
}
