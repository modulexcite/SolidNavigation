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
            var pattern = Regex.Replace(UrlPattern, @"{(.*?)}", @"([\w]*)");
            var match = Regex.Match(url, pattern);
            return match.Success;
        }

        public List<UrlSegment> Segments {
            get {
                var segments = new List<UrlSegment>();
                foreach (var segment in UrlPattern.Split('/')) {
                    if (segment.StartsWith("{") && segment.EndsWith("}")) {
                        segments.Add(new UrlSegment { Segment = segment.Replace("{","").Replace("}",""), IsVariable = true });
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