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
            if (Target is TaskDetailsTarget)
            {
                DataContext = new TaskDetailsViewModel(((TaskDetailsTarget) Target).TaskId);
            }
            else
            {
                var target = (CommentsTarget) Target;
                DataContext = new TaskDetailsViewModel(target.TaskId, target.Id);
            }

            NavigateService.Current.MasterView.ShowTarget(e.Parameter + "");
        }

        private void OnCommentsButtonClick(object sender, RoutedEventArgs e)
        {
            var taskId = ((TaskDetailsTarget)Target).TaskId;
            NavigateService.Current.Navigate(new CommentsTarget(taskId,45));
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateService.Current.GoBack();
        }
    }
}
