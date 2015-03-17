using System.Diagnostics;

namespace SolidNavigation.Sdk
{
    [DebuggerDisplay("Target: {GetType().Name}")]
    public abstract class NavigationTarget
    {
        public override string ToString()
        {
            return GetType().Name;
        }

        public override int GetHashCode()
        {
            var url = Router.Current.CreateUrl(this);
            return url.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as NavigationTarget;
            if (other == null || other.GetHashCode() != GetHashCode())
            {
                return false;
            }
            return true;
        }
    }
}