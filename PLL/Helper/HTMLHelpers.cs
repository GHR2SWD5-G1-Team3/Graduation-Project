using Microsoft.AspNetCore.Mvc.Rendering;

namespace PLL.Helper
{
    public static class HTMLHelpers
    {
        public static string IsActive(this IHtmlHelper html, string? ctrl = null, string? act = null)
        {
            var routeData = html.ViewContext.RouteData;
            var routeCtrl = routeData.Values["controller"]?.ToString();
            var routeAct = routeData.Values["action"]?.ToString();

            if (ctrl == routeCtrl && (act == null || act == routeAct))
                return "active";

            return "";
        }

        public static string IsExpandedGroup(this IHtmlHelper html, params string[] controllers)
        {
            var routeData = html.ViewContext.RouteData;
            string? currentController = routeData.Values["controller"]?.ToString();

            return controllers.Contains(currentController) ? "show" : "";
        }

        public static string IsParentActive(this IHtmlHelper html, params string[] controllers)
        {
            var routeData = html.ViewContext.RouteData;
            string? currentController = routeData.Values["controller"]?.ToString();

            return controllers.Contains(currentController) ? "active" : "";
        }
    }
}
