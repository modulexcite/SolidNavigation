namespace SolidNavigation.Sdk
{
    public abstract class NavigationServiceBase
    {
        public void Navigate(NavigationTarget target)
        {
            var route = Router.Current.FindRoute(target);
            var url = Router.Current.CreateUrl(target);
            Navigate(route, target, url);
        }

        public void Navigate(string uri)
        {
            var target = Router.Current.CreateTarget(uri);
            if (target != null)
            {
                var route = Router.Current.FindRoute(target);
                Navigate(route, target, uri);
            }
        }

        protected abstract void Navigate(Route route, NavigationTarget target, string uri);
    }
}
