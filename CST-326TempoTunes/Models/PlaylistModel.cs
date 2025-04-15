using System.Collections.Generic;

namespace CST_326TempoTunes.Models
{
    public class PlaylistModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string ImageUrl { get; set; }
        public List<TrackModel> Tracks { get; set; } = new List<TrackModel>();
    }
}
