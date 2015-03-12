using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SolidNavigation.Sdk
{
    [DebuggerDisplay("TargetType: {TargetType.Name}, UrlPattern: {UrlPattern}, PageType: {PageType.Name}")]
    public class Route
    {
        public Route(string urlpattern, Type pageType, Type targetType)
        {
            UrlPattern = urlpattern;
            PageType = pageType;
            TargetType = targetType;
        }

        public string UrlPattern { get; private set; }
        public Type PageType { get; set; }
        public Type TargetType { get; private set; }

        public bool IsMatch(string url)
        {
            if (string.IsNullOrEmpty(UrlPattern))
                return true;

            var pattern = Regex.Replace(UrlPattern, @"{(.*?)}", @"([\w%]*)");
            var match = Regex.Match(url, pattern);
            return match.Success;
        }

        public List<UrlSegment> Segments
        {
            get
            {
                var segments = new List<UrlSegment>();
                foreach (var segment in UrlPattern.Split('/'))
                {
                    if (segment.StartsWith("{") && segment.EndsWith("}"))
                    {
                        segments.Add(new UrlSegment { Segment = segment.Replace("{", "").Replace("}", ""), IsVariable = true });
                    }
                    else
                    {
                        segments.Add(new UrlSegment { Segment = segment });
                    }
                }
                return segments;
            }
        }
    }

    [DebuggerDisplay("Segment: {Segment}, IsVariable: {IsVariable}")]
    public class UrlSegment
    {
        public string Segment { get; set; }
        public bool IsVariable { get; set; } // determined if segment contains { and }
    }
}