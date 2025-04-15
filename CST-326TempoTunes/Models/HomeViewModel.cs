using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace CST_326TempoTunes.Models
{
    /// <summary>
    /// Even though there are alternatives, creating a dedicated view model (like HomeViewModel) is a common best practice when:
        /// You need type safety in your views.
        /// You want your code to be self-documenting (i.e., HomeViewModel clearly states the data required for the Home page)
        /// You anticipate that the view might need more properties or logic in the future.
    /// A view model also makes it easier to manage validation or presentation-specific properties that don’t map directly to your database models.If you’re working on a small or quick project, it might feel like extra overhead. But in larger applications, it helps keep things organized.
    /// </summary>
    public class HomeViewModel
    {
        public List<PlaylistModel> Playlists { get; set; }
        public List<TrackModel> Tracks { get; set; }
    }
}
