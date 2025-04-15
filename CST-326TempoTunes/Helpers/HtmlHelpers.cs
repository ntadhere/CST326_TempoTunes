using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CST_326TempoTunes.Helpers
{
    // A static class for custom HTML helper extension methods.
    public static class HtmlHelpers
    {
        /// <summary>
        /// Returns the specified CSS class ("active" by default) if the current route matches the given controller and action names.
        /// </summary>
        /// <param name="html">The IHtmlHelper instance.</param>
        /// <param name="controllers">A comma-separated list of accepted controller names.</param>
        /// <param name="actions">A comma-separated list of accepted action names.</param>
        /// <param name="cssClass">The CSS class to return if the route matches. Defaults to "active".</param>
        /// <returns>The CSS class if the current route matches; otherwise, an empty string.</returns>
        public static string IsActive(this IHtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
        {
            // Retrieve the current controller and action from the route data.
            var viewContext = html.ViewContext;
            var currentController = viewContext.RouteData.Values["controller"]?.ToString();
            var currentAction = viewContext.RouteData.Values["action"]?.ToString();

            // Split the provided controller and action names into arrays, allowing for multiple values.
            var acceptedControllers = controllers.Split(',').Select(x => x.Trim());
            var acceptedActions = actions.Split(',').Select(x => x.Trim());

            // If both the current controller and action are among the accepted values, return the CSS class.
            if (acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction))
            {
                return cssClass;
            }
            // Otherwise, return an empty string.
            return string.Empty;
        }
    }
}
