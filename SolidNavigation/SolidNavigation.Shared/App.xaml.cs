using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using SolidNavigation.Sdk;

namespace SolidNavigation
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;

            Router.Current.Scheme = "solidnavigation://";
            Router.Current.AddRoute("tasks/{taskid}/comments", typeof(TaskDetailsPage), typeof(CommentsTarget));
            Router.Current.AddRoute("tasks/{taskid}", typeof(TaskDetailsPage), typeof(TaskDetailsTarget));
            Router.Current.AddRoute("lists/{listid}", typeof(TasksPage), typeof(TaskListTarget));
            Router.Current.AddRoute("", typeof(ListsPage), typeof(HomeTarget));
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

            var masterView = Window.Current.Content as MasterView;
            if (masterView == null)
            {
                masterView = new MasterView();
                Window.Current.Content = masterView;
            }

            NavigateService.Current.Navigate(new HomeTarget());
            Window.Current.Activate();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}