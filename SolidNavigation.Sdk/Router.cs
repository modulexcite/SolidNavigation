using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var uriInfo = new UriInfo(url);
            var route = FindRoute(uriInfo.Path);

            var parameterValues = new List<object>();

            var cons = GetConstructor(route, uriInfo);

            if (cons == null)
            {
                // no matching constructor for NavigationTarget
                return null;
            }

            var ctorparams = cons.GetParameters();
            var counter = 0;
            for (int i = 0; i < route.Segments.Count; i++) {
                if (route.Segments[i].IsVariable) {
                    var value = Convert.ChangeType(uriInfo.Segments[i], ctorparams[counter].ParameterType);
                    parameterValues.Add(value);
                    counter++;
                }
            }
            foreach (var qs in uriInfo.QueryString)
            {
                var value = Convert.ChangeType(qs.Value, ctorparams[counter].ParameterType);
                parameterValues.Add(value);
                counter++;
            }

            var target = cons.Invoke(parameterValues.ToArray()) as NavigationTarget;
            return target;
        }

        private ConstructorInfo GetConstructor(Route route, UriInfo uriInfo)
        {
            var segments = route.Segments.Where(x => x.IsVariable).Select(x => x.Segment.ToLower()).ToList();
            foreach (var qs in uriInfo.QueryString)
            {
                segments.Add(qs.Key);
            }
            var constructors = route.TargetType.GetTypeInfo().DeclaredConstructors;
            foreach (var constructor in constructors) {
                if (IsParameterMatch(constructor.GetParameters(), segments)) {
                    return constructor;
                }
            }
            return null;
        }

        private bool IsParameterMatch(ParameterInfo[] ctorParameters, List<string> parameterNames) {
            if (ctorParameters.Count() != parameterNames.Count) {
                return false;
            }
            return ctorParameters.All(ctorParameter => parameterNames.Contains(ctorParameter.Name.ToLower()));
        }

        private Route FindRoute(string path) {
            var route = _routes.FirstOrDefault(r => r.IsMatch(path));
            return route;
        }
    }

    [DebuggerDisplay("Path: {Path}, Segments: {Segments.Count}, QueryString: {QueryString.Count}")]
    public class UriInfo {
        public UriInfo(string url) {
            var urlparts = url.Split(new[] { "://" }, StringSplitOptions.None);
            var hierarchy = urlparts[1];
            var pathparts = hierarchy.Split('?');

            _path = pathparts[0].TrimStart('/').TrimEnd('/');
            var pathsegments = _path.Split('/');
            foreach (var pathsegment in pathsegments) {
                _segments.Add(pathsegment);
            }

            if (pathparts.Length > 1) {
                var querystring = pathparts[1];
                var querystringparts = querystring.Split('&');
                foreach (var querystringpart in querystringparts) {
                    var keyvalue = querystringpart.Split('=');
                    _queryString.Add(keyvalue[0], keyvalue[1]);
                }
            }
        }

        private readonly List<string> _segments = new List<string>();
        private readonly Dictionary<string, string> _queryString = new Dictionary<string, string>();
        private string _path;

        public List<string> Segments { get { return _segments; } }
        public Dictionary<string, string> QueryString { get { return _queryString; } }

        public string Path{get { return _path; }}
    }
}