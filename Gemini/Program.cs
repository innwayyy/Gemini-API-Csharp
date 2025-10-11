using System.Net.Http.Json;
using System.Text.Json;

class Program
{
   static async public Task<string> TextAsync(string prompt)
    {
        var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";
        var apiKey = "APIKEY_FROM_GOOGLE_AISTUDIO";
        var body = new
        {
            contents = new[]
            {
        new
        {
            parts = new[]
            {
                new { text =prompt  },
            }
        }
    }
        };
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

        var response = await httpClient.PostAsJsonAsync(url, body);
        var responseText = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseText);
        var text = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        Console.WriteLine(text);
        return text ?? "idk bro";
    }
    private static async Task Main(string[] args)
    {
        string prompt = Console.ReadLine()!;
        Console.WriteLine("---------------------");
        await TextAsync(prompt);
    }
}