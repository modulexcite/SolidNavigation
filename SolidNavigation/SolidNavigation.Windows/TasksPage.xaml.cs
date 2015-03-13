using System;
using Windows.Foundation;
using Windows.UI.StartScreen;
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

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var route = Router.Current.FindRoute(Target);
            var url = Router.Current.CreateUrl(route, Target);
            var logo = new Uri("ms-appx:///Assets/Logo.png");

            var target = (TaskListTarget)Target;

            var secondaryTile = new SecondaryTile(Guid.NewGuid() + "", "list " + target.ListId, url, logo, TileSize.Square150x150);
            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
            await secondaryTile.RequestCreateAsync(new Point(ActualWidth / 2, ActualHeight / 2));
        }
    }
}
