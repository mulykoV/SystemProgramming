using System;
using System.Collections.Generic;

namespace MovieApp;

public class Movie
{
    // 4 властивості 
    public string Title { get; set; } = "";        
    public int ReleaseYear { get; set; }           
    public double Rating { get; set; }             
    public List<string> Genres { get; set; } = new(); 

    // 2 Конструктори
    public Movie() { }

    public Movie(string title, int year, double rating, List<string> genres)
    {
        Title = title;
        ReleaseYear = year;
        Rating = rating;
        Genres = genres;
    }

    // 3 Методи 
    public string GetShortInfo() => $"{Title} ({ReleaseYear})";

    public void AddGenre(string genre) => Genres.Add(genre);

    public bool IsHighlyRated() => Rating > 8.0;
}