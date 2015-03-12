using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation
{
    public sealed partial class CommentsPage
    {
        public CommentsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new CommentsPageViewModel(((CommentsTarget)Target).TaskId);
            NavigateService.Current.MasterView.ShowTarget(e.Parameter + "");
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateService.Current.GoBack();
        }
    }
}
