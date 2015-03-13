using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using SolidNavigation.Sdk;

namespace SolidNavigation
{
    public sealed partial class TasksPage
    {
        public TasksPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new TasksPageViewModel(((TaskListTarget)Target).ListId);
            NavInfo.Text = e.Parameter + "\n" + Target;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateService.Current.GoBack();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var route = Router.Current.FindRoute(Target);
            var url = Router.Current.CreateUrl(route, Target);
        }
    }
}
