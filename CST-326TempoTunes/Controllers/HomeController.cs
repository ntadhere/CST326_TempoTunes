using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.Business;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CST_326TempoTunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var playlistCollection = new PlaylistCollection();

            // Retrieve all playlists for the carousel
            var playlists = playlistCollection.GetAllPlaylists();

            // Retrieve all tracks to list below the carousel
            var tracks = playlistCollection.GetAllTracks();

            // Combine them in a single view model
            var model = new HomeViewModel
            {
                Playlists = playlists,
                Tracks = tracks
            };

            return View(model);
        }

        public IActionResult Playlist()
        {
            // Retrieve playlists from the business layer
            PlaylistCollection playlistCollection = new PlaylistCollection();
            List<PlaylistModel> playlists = playlistCollection.GetAllPlaylists();
            return View(playlists);
        }

        public IActionResult OnePlaylist(int id)
        {
            // Use the business layer to get the chosen playlist
            PlaylistCollection playlistCollection = new PlaylistCollection();
            // Retrieve the playlist data (without tracks) by filtering the full list.
            PlaylistModel playlist = playlistCollection.GetAllPlaylists().FirstOrDefault(p => p.Id == id);

            if (playlist != null)
            {
                // Load the tracks dynamically for the chosen playlist
                playlist.Tracks = playlistCollection.GetTracksForPlaylist(id);
                return View(playlist);
            }

            // If playlist not found, return a NotFound result.
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddPlaylist(PlaylistModel playlist)
        {
            var playlistCollection = new PlaylistCollection();
            bool success = playlistCollection.AddPlaylist(playlist);
            return RedirectToAction("Playlist");
        }

        [HttpPost]
        public IActionResult AddTrack(TrackModel track, int playlistId)
        {
            var playlistCollection = new PlaylistCollection();
            bool success = playlistCollection.AddTrack(track, playlistId);
            return RedirectToAction("OnePlaylist", new { id = playlistId });
        }


        [HttpPost]
        public IActionResult RemovePlaylist(int id)
        {
            var playlistCollection = new PlaylistCollection();
            bool success = playlistCollection.RemovePlaylist(id);
            if (success)
            {
                // Redirect to the playlist list or wherever you want
                return RedirectToAction("Playlist");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveTrack(int trackId, int playlistId)
        {
            var playlistService = new PlaylistCollection();
            bool success = playlistService.RemoveTrack(trackId);
            if (success)
            {
                return RedirectToAction("OnePlaylist", new { id = playlistId });
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
