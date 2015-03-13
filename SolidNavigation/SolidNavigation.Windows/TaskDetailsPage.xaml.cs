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
                DataContext = new TaskDetailsViewModel(((TaskDetailsTarget)Target).TaskId);
            }
            else
            {
                var target = (CommentTarget)Target;
                DataContext = new TaskDetailsViewModel(target.TaskId, target.CommentId);
            }

            NavInfo.Text = e.Parameter + "\n" + Target;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateService.Current.GoBack();
        }
    }
}
