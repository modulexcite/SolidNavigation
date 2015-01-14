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

        public string Protocol {
            get {
                if (_protocol == null) {
                    throw new Exception("Set routing protocol. (Router.Current.Protocol = \"myfabulousapp://\";)");
                }
                return _protocol;
            }
            set { _protocol = value; }
        }

        public string CreateUrl(NavigationTarget target) {
            var route = _routes.FirstOrDefault(x => x.TargetType == target.GetType());
            var url = Protocol + route.UrlPattern;
            var props = target.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var prop in props) {
                var value = prop.GetValue(target) + "";
                url = url.Replace("{" + prop.Name.ToLower() + "}", value);
            }
            return url;
        }

        public NavigationTarget CreateTarget(string url) {
            var pattern = url.Replace(Protocol, "");

            var parts = pattern.Split('?');
            var path = parts[0].TrimStart('/').TrimEnd('/');

            var route = FindRoute(path);

            var args = new object[] { };
            if (pattern != "") {
                var patternparts = pattern.Split('/');
                args = new object[] { Int64.Parse(patternparts[1]) };
            }
            var cons = route.TargetType.GetTypeInfo().DeclaredConstructors.First();
            var target = cons.Invoke(args) as NavigationTarget;
            return target;
        }

        private Route FindRoute(string path) {
            var route = _routes.FirstOrDefault(r => r.IsMatch(path));
            return route;
        }
    }
}