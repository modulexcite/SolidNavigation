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
            Router.Current.AddRoute("tasks/{taskid}/comments", typeof(TaskDetailsPage), typeof(CommentTarget));
            Router.Current.AddRoute("tasks/{taskid}", typeof(TaskDetailsPage), typeof(TaskDetailsTarget));
            Router.Current.AddRoute("lists/{listid}", typeof(TasksPage), typeof(TaskListTarget));
            Router.Current.AddRoute("", typeof(ListsPage), typeof(HomeTarget));
        }

        // paste in explorer: solidnavigation://tasks/2/comments?commentid=8
        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
            if (args.Kind == ActivationKind.Protocol)
            {
                var protocolArgs = (ProtocolActivatedEventArgs)args;
                NavigateService.Current.Navigate(protocolArgs.Uri + "");
                Window.Current.Activate();
            }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var masterView = Window.Current.Content as MasterView;
            if (masterView == null)
            {
                masterView = new MasterView();
                Window.Current.Content = masterView;
            }

            NavigateService.Current.Navigate(e.Arguments);

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