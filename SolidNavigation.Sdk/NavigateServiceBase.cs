using System;

namespace SolidNavigation.Sdk {
    public abstract class NavigateServiceBase {
        public abstract void GoBack();
        protected abstract void Navigate(Type pageType, object parameter);

        public void Navigate(NavigationTarget target) {
            Navigate(target.PageType, Router.Current.CreateUrl(target));
        }

        public void Navigate(string url) {
            var target = Router.Current.CreateTarget(url);
            if (target != null) {
                Navigate(target.PageType, url);
            }
        }
    }
}
