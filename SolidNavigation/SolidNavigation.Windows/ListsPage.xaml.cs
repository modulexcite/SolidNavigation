using Windows.UI.Xaml.Navigation;

namespace SolidNavigation
{
    public sealed partial class ListsPage
    {
        public ListsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new ListsPageViewModel();

            var ttt = Target;
            NavigateService.Current.MasterView.ShowTarget(e.Parameter + "");
        }
    }
}
