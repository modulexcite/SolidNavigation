using Windows.UI.Xaml.Navigation;

namespace SolidNavigation {
    public sealed partial class TasksPage {
        public TasksPage() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            DataContext = new TasksPageViewModel(((TaskListTarget)Target).ListId);
            NavigateService.Current.MasterView.ShowTarget(e.Parameter + "");
        }
    }
}
