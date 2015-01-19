using System;
using System.Diagnostics;

namespace SolidNavigation.Sdk {
    [DebuggerDisplay("PageType: {PageType}, _data-count: {_data.Count}")]
    public abstract class NavigationTarget {
        public abstract Type PageType { get; }
        public override string ToString() {
            return GetType().Name;// + " " + par;
        }
    }
}