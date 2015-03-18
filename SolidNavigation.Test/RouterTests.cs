using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SolidNavigation.Sdk;

namespace SolidNavigation.Test
{
    [TestClass]
    public class RouterTests
    {
        private class SearchTarget : NavigationTarget
        {
            public SearchTarget(string searchText)
            {
                SearchText = searchText;
            }

            public string SearchText { get; private set; }
        }

        [TestMethod]
        public void Parse_url_with_segments()
        {
            var router = CreateRouter().AddRoute("lists/{listid}", null, typeof(TaskListTarget));

            var target = router.CreateTarget("test://lists/123");

            Assert.IsInstanceOfType(target, typeof(TaskListTarget));
            Assert.AreEqual(123, ((TaskListTarget)target).ListId);
        }

        [TestMethod]
        public void Create_url_with_segments()
        {
            var router = CreateRouter().AddRoute("lists/{listid}", null, typeof(TaskListTarget));

            var target = new TaskListTarget(123);
            var url = router.CreateUrl(target);

            Assert.AreEqual("test://lists/123", url);
        }

        [TestMethod]
        public void Parse_url_with_query_params()
        {
            var router = CreateRouter().AddRoute("search", null, typeof(SearchTarget));

            var target = router.CreateTarget("test://search?searchtext=two%20words");

            Assert.IsInstanceOfType(target, typeof(SearchTarget));
            Assert.AreEqual("two words", ((SearchTarget)target).SearchText);
        }

        [TestMethod]
        public void Create_url_with_query_params()
        {
            var router = CreateRouter().AddRoute("search", null, typeof(SearchTarget));

            var target = new SearchTarget("two words");
            var url = router.CreateUrl(target);

            Assert.AreEqual("test://search?searchtext=two%20words", url);
        }

        [TestMethod]
        public void Parse_emtpy_url_should_match_empty_route()
        {
            var router = CreateRouter().AddRoute("foobar", null, typeof(SearchTarget))
                                       .AddRoute("", null, typeof(HomeTarget));

            var target = router.CreateTarget(string.Empty);

            Assert.IsInstanceOfType(target, typeof(HomeTarget));
        }

        [TestMethod]
        public void Parse_unknown_url_should_match_empty_route()
        {
            var router = CreateRouter().AddRoute("", null, typeof(HomeTarget));

            var target = router.CreateTarget("test://unknown-route");

            Assert.IsInstanceOfType(target, typeof(HomeTarget));
        }

        [TestMethod]
        public void When_url_does_not_contain_a_scheme_then_should_throw()
        {
            const string url = "S/95368018";
            Assert.ThrowsException<ArgumentException>(() => { var info = new UriInfo(url); });
        }

        [TestMethod]
        public void When_url_contains_a_scheme_then_should_not_throw()
        {
            const string url = "test://S/95368018";
            var info = new UriInfo(url);
            Assert.AreEqual(2, info.Segments.Count);
        }

        private static Router CreateRouter()
        {
            return new Router { Scheme = "test://" };
        }
    }
}
