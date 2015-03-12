using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation
{
    public sealed partial class TaskDetailsPage
    {
        public TaskDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new TaskDetailsViewModel(((TaskDetailsTarget)Target).TaskId);
            NavigateService.Current.MasterView.ShowTarget(e.Parameter + "");
        }

        private void OnCommentsButtonClick(object sender, RoutedEventArgs e)
        {
            var taskId = ((TaskDetailsTarget)Target).TaskId;
            NavigateService.Current.Navigate(new CommentsTarget(taskId));
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateService.Current.GoBack();
        }
    }
}
