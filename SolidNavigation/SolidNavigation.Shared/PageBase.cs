using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SolidNavigation.Sdk;  

namespace SolidNavigation {
    public class PageBase : Page {
        public NavigationTarget Target { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            var url = e.Parameter + "";
            Target = Router.Current.CreateTarget(url);

            // do something with NavigationTarget
        }
    }
}
