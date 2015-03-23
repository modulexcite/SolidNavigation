using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using SolidNavigation.Windows.Navigation;

namespace SolidNavigation.Windows.Tasks
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
    }
}
