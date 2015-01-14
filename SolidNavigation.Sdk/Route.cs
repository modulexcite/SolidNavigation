using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SolidNavigation.Sdk {
    [DebuggerDisplay("TargetType: {TargetType}, UrlPattern: {UrlPattern}")]
    public class Route {
        public Route(string urlpattern, Type targeType) {
            TargetType = targeType;
            UrlPattern = urlpattern;
        }
        public Type TargetType { get; private set; }
        public string UrlPattern { get; private set; }

        public bool IsMatch(string url) {
            var pattern = Regex.Replace(UrlPattern, @"\{([^)]+)\}", @"([\w]*)");
            var match = Regex.Match(url, pattern);
            return match.Success;
        }
    }
}