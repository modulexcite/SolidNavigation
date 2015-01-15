using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SolidNavigation.Sdk {
    [DebuggerDisplay("PageType: {PageType}, _data-count: {_data.Count}")]
    public abstract class NavigationTarget {
        protected readonly Dictionary<string, object> _data = new Dictionary<string, object>();
        public abstract Type PageType { get; }
        public override string ToString() {
            var par = "";
            foreach (var o in _data) {
                par += "\nulr-param: " + o.Key + "=" + o.Value + " ";
            }
            return GetType().Name + " " + par;
        }
    }
}