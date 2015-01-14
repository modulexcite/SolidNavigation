using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using SolidNavigation.Sdk;

namespace SolidNavigation {
    public sealed partial class App {
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        public App() {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            Router.Current.Protocol = "solidnavigation://";
            Router.Current.AddRoute("tasks/{taskid}/comments", typeof(CommentsTarget));
            Router.Current.AddRoute("tasks/{taskid}", typeof(TaskDetailsTarget));
            Router.Current.AddRoute("lists/{listid}", typeof(TaskListTarget));
            Router.Current.AddRoute("", typeof(HomeTarget));
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e) {

            var masterView = Window.Current.Content as MasterView;
            if (masterView == null) {
                masterView = new MasterView();
                Window.Current.Content = masterView;
            }

            NavigateService.Current.Navigate(new HomeTarget());

            // Ensure the current window is active
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        private void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}