using Windows.UI.Xaml.Navigation;

namespace SolidNavigation {
    public sealed partial class ListsPage {
        public ListsPage() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            DataContext = new ListsPageViewModel();

            NavigateService.Current.MasterView.ShowTarget( e.Parameter + "");
        }
    }
}
