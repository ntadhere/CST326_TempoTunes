using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.Business;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CST_326TempoTunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlaylistCollection _playlistService;

        // Inject both logger and your service layer
        public HomeController(ILogger<HomeController> logger,
                              PlaylistCollection playlistService)
        {
            _logger = logger;
            _playlistService = playlistService;
        }

        public IActionResult Index()
        {
            var playlists = _playlistService.GetAllPlaylists();
            var tracks = _playlistService.GetAllTracks();

            var model = new HomeViewModel
            {
                Playlists = playlists,
                Tracks = tracks
            };

            return View(model);
        }

        public IActionResult Playlist()
        {
            var playlists = _playlistService.GetAllPlaylists();
            return View(playlists);
        }

        public IActionResult OnePlaylist(int id)
        {
            var playlist = _playlistService
                               .GetAllPlaylists()
                               .FirstOrDefault(p => p.Id == id);

            if (playlist == null)
                return NotFound();

            playlist.Tracks = _playlistService.GetTracksForPlaylist(id);
            return View(playlist);
        }

        [HttpPost]
        public IActionResult AddPlaylist(PlaylistModel playlist)
        {
            if (_playlistService.AddPlaylist(playlist))
                return RedirectToAction("Playlist");

            // You might want to handle failure more gracefully
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddTrack(TrackModel track, int playlistId)
        {
            if (_playlistService.AddTrack(track, playlistId))
                return RedirectToAction("OnePlaylist", new { id = playlistId });

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPlaylist(PlaylistModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Playlist");  // Or return view with errors

            bool ok = _playlistService.UpdatePlaylist(vm);
            TempData["Message"] = ok ? "Playlist updated." : "Update failed.";
            return RedirectToAction("Playlist");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTrack(TrackModel vm, int playlistId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("OnePlaylist", new { id = playlistId });

            vm.PlaylistId = playlistId;
            bool ok = _playlistService.UpdateTrack(vm);
            TempData["Message"] = ok ? "Track updated." : "Update failed.";
            return RedirectToAction("OnePlaylist", new { id = playlistId });
        }


        [HttpPost]
        public IActionResult RemovePlaylist(int id)
        {
            if (_playlistService.RemovePlaylist(id))
                return RedirectToAction("Playlist");

            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveTrack(int trackId, int playlistId)
        {
            if (_playlistService.RemoveTrack(trackId))
                return RedirectToAction("OnePlaylist", new { id = playlistId });

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
