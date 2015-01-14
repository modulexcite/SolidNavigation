using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SolidNavigation.Sdk {
    [DebuggerDisplay("TargetType: {TargetType}, UrlPattern: {UrlPattern}")]
    public class Route {
        public Route(string urlpattern, Type targetType) {
            TargetType = targetType;
            UrlPattern = urlpattern;
        }
        public Type TargetType { get; private set; }
        public string UrlPattern { get; private set; }

        public bool IsMatch(string url) {
            var pattern = Regex.Replace(UrlPattern, @"\{([^)]+)\}", @"([\w]*)"); // als dit niet werkt, neem dan regex van Paramnames
            var match = Regex.Match(url, pattern);
            return match.Success;
        }

        //public List<string> ParamNames {
        //    get {
        //        var names = new List<string>();
        //        var patt = new Regex(@"{(.*?)}");
        //        foreach (Match match in patt.Matches(UrlPattern)) {
        //            names.Add(match.Groups[1].Value);
        //        }
        //        return names;
        //    }
        //}

        public List<UrlSegment> Segments {
            get {
                var segments = new List<UrlSegment>();
                foreach (var segment in UrlPattern.Split('/')) {
                    if (segment.StartsWith("{") && segment.EndsWith("}")) {
                        segments.Add(new UrlSegment { Segment = segment, IsVariable = true });
                    } else {
                        segments.Add(new UrlSegment { Segment = segment });
                    }
                }
                return segments;
            }
        }
    }

    [DebuggerDisplay("Segment: {Segment}, IsVariable: {IsVariable}")]
    public class UrlSegment {
        public string Segment { get; set; }
        public bool IsVariable { get; set; }
    }
}