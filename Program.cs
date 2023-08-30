using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

class Program
{
    private const string OmdbApiBaseUrl = "http://www.omdbapi.com";
    private const string OmdbApiKey = "79b7d55d";

    static async Task Main()
    {
        Console.Write("Enter Movie Title: ");
        string movieTitle = Console.ReadLine();

        using (var httpClient = new HttpClient())
        {
            var requestUrl = $"{OmdbApiBaseUrl}?apikey=79b7d55d&t={Uri.EscapeDataString(movieTitle)}";
            var response = await httpClient.GetAsync(requestUrl);
            var content = await response.Content.ReadAsStringAsync();

            var movie = JsonSerializer.Deserialize<Movie>(content);

            if (movie != null && !string.IsNullOrEmpty(movie.Title))
            {
                Console.WriteLine($"Title: {movie.Title}");
                Console.WriteLine($"Year: {movie.Year}");
                Console.WriteLine($"Plot: {movie.Plot}");
            }
            else
            {
                Console.WriteLine($"Movie with title '{movieTitle}' not found.");
            }
        }
    }
}

public class Movie
{
    [JsonPropertyName("Title")]
    public string Title { get; set; }

    [JsonPropertyName("Year")]
    public string Year { get; set; }

    [JsonPropertyName("Plot")]
    public string Plot { get; set; }
}

