using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SolidNavigation.Sdk
{
    public class Router
    {
        private readonly List<Route> _routes = new List<Route>();
        private string _scheme;
        private static Router _current;
        public static Router Current { get { return _current ?? (_current = new Router()); } }

        public void AddRoute(string urlPattern, Type pageType, Type targetType)
        {
            _routes.Add(new Route(urlPattern, pageType, targetType));
        }

        public string Scheme
        {
            get
            {
                if (_scheme == null)
                {
                    throw new Exception("Set routing scheme. (Router.Current.Scheme = \"myfabulousapp://\";)");
                }
                return _scheme;
            }
            set { _scheme = value; }
        }

        public string CreateUrl(Route route, NavigationTarget target)
        {
            var url = Scheme + route.UrlPattern;
            var props = target.GetType().GetPropertiesHierarchical();
            var routeVars = route.Segments.Where(x => x.IsVariable).Select(x => x.Segment.ToLower());
            foreach (var prop in props)
            {
                var value = Uri.EscapeDataString(prop.GetValue(target) + "");
                if (routeVars.Contains(prop.Name.ToLower()))
                {
                    url = url.Replace("{" + prop.Name.ToLower() + "}", value);
                }
                else if (prop.PropertyType == typeof(string) || prop.PropertyType.GetTypeInfo().IsValueType)
                {
                    url += url.Contains("?") ? "&" : "?";
                    url += prop.Name.ToLower() + "=" + value;
                }
            }
            return url;
        }

        public NavigationTarget CreateTarget(string url)
        {
            if (!url.StartsWith(Scheme))
            {
                url = string.Format("{0}{1}", Scheme, url);
            }

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
            for (int i = 0; i < route.Segments.Count; i++)
            {
                if (route.Segments[i].IsVariable)
                {
                    parameterValues.Add(ChangeType(uriInfo.Segments[i], ctorparams[counter].ParameterType));
                    counter++;
                }
            }
            foreach (var qs in uriInfo.QueryString)
            {
                parameterValues.Add(ChangeType(qs.Value, ctorparams[counter].ParameterType));
                counter++;
            }

            var target = cons.Invoke(parameterValues.ToArray()) as NavigationTarget;
            return target;
        }

        private static object ChangeType(string value, Type type)
        {
            return Convert.ChangeType(Uri.UnescapeDataString(value), type);
        }

        private ConstructorInfo GetConstructor(Route route, UriInfo uriInfo)
        {
            var urlVariables = route.Segments.Where(x => x.IsVariable).Select(x => x.Segment.ToLower()).ToList();
            foreach (var qs in uriInfo.QueryString)
            {
                urlVariables.Add(qs.Key);
            }
            var constructors = route.TargetType.GetTypeInfo().DeclaredConstructors;
            foreach (var constructor in constructors)
            {
                if (IsParameterMatch(constructor.GetParameters(), urlVariables))
                {
                    return constructor;
                }
            }
            return null;
        }

        private static bool IsParameterMatch(ParameterInfo[] ctorParameters, List<string> parameterNames)
        {
            if (ctorParameters.Count() != parameterNames.Count)
            {
                return false;
            }
            return ctorParameters.All(ctorParameter => parameterNames.Contains(ctorParameter.Name.ToLower()));
        }

        public Route FindRoute(NavigationTarget target)
        {
            var route = _routes.FirstOrDefault(x => x.TargetType == target.GetType());
            if (route == null)
            {
                throw new Exception("No route for NavigationTarget: " + target.GetType().Name);
            }
            return route;
        }

        public Route FindRoute(string path)
        {
            var route = _routes.FirstOrDefault(r => r.IsMatch(path));
            if (route == null)
            {
                throw new Exception("No route for: " + path);
            }
            return route;
        }
    }

    [DebuggerDisplay("Path: {Path}, Segments: {Segments.Count}, QueryString: {QueryString.Count}")]
    public class UriInfo
    {
        public UriInfo(string url)
        {
            if (!url.Contains("://"))
                throw new ArgumentException("The argument 'url' does not contain a scheme!");

            var urlparts = url.Split(new[] { "://" }, StringSplitOptions.None);
            var hierarchy = urlparts[1];
            var pathparts = hierarchy.Split('?');

            _path = pathparts[0].TrimStart('/').TrimEnd('/');
            var pathsegments = _path.Split('/');
            foreach (var pathsegment in pathsegments)
            {
                _segments.Add(pathsegment);
            }

            if (pathparts.Length > 1)
            {
                var querystring = pathparts[1];
                var querystringparts = querystring.Split('&');
                foreach (var querystringpart in querystringparts)
                {
                    var keyvalue = querystringpart.Split('=');
                    _queryString.Add(keyvalue[0], keyvalue[1]);
                }
            }
        }

        private readonly List<string> _segments = new List<string>();
        private readonly Dictionary<string, string> _queryString = new Dictionary<string, string>();
        private readonly string _path = string.Empty;

        public List<string> Segments { get { return _segments; } }
        public Dictionary<string, string> QueryString { get { return _queryString; } }

        public string Path { get { return _path; } }
    }
}
