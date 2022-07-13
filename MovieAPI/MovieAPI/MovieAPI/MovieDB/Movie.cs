using System;
using System.Collections.Generic;

namespace MovieAPI.MovieDB
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; } = null!;
        public int MovieRating { get; set; }
        public string MovieCategory { get; set; } = null!;
        public string MovieLanguage { get; set; } = null!;
        public string MovieContent { get; set; } = null!;
        public string MovieImage { get; set; } = null!;
    }
}
