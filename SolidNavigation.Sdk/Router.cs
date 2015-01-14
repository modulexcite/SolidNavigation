using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SolidNavigation.Sdk {
    public class Router {
        private readonly List<Route> _routes = new List<Route>();
        private string _protocol;
        private static Router _current;
        public static Router Current { get { return _current ?? (_current = new Router()); } }

        public void AddRoute(string urlPattern, Type pageType) {
            _routes.Add(new Route(urlPattern, pageType));
        }

        public string Scheme {
            get {
                if (_protocol == null) {
                    throw new Exception("Set routing scheme. (Router.Current.Scheme = \"myfabulousapp://\";)");
                }
                return _protocol;
            }
            set { _protocol = value; }
        }

        public string CreateUrl(NavigationTarget target) {
            var route = _routes.FirstOrDefault(x => x.TargetType == target.GetType());
            if (route == null) {
                throw new Exception("Add route for NavigationTarget: " + target.GetType().Name);
            }
            var url = Scheme + route.UrlPattern;
            var props = target.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var prop in props) {
                var value = prop.GetValue(target) + "";
                url = url.Replace("{" + prop.Name.ToLower() + "}", value);
            }
            return url;
        }

        public NavigationTarget CreateTarget(string url) {
            var uri = new Uri(url);


            var pattern = url.Replace(Scheme, "");

            var parts = pattern.Split('?');
            var path = parts[0].TrimStart('/').TrimEnd('/');

            var urlsegments = path.Split('/');

            var route = FindRoute(path);

            // niet met constructor doen
            // route vinden en dan alle parameters uit de URL vissen

            var cag = new List<object>();
            var cons = route.TargetType.GetTypeInfo().DeclaredConstructors.First();
            var ctorparams = cons.GetParameters();
            var counter = 0;
            for (int i = 0; i < route.Segments.Count; i++) {
                if (route.Segments[i].IsVariable)
                {
                    var value = Convert.ChangeType(urlsegments[i], ctorparams[counter].ParameterType);
                    cag.Add(value);
                    counter++;
                }
            }

            //var args = new object[] { };
            //if (path != "") {
            //    var patternparts = path.Split('/');
            //    args = new object[] { Int64.Parse(patternparts[1]) };
            //}
            var target = cons.Invoke(cag.ToArray()) as NavigationTarget;
            return target;
        }

        private Route FindRoute(string path) {
            var route = _routes.FirstOrDefault(r => r.IsMatch(path));
            return route;
        }
    }
}