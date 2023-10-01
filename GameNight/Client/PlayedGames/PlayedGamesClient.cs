using GameNight.Shared;
using System.Net.Http.Json;

namespace GameNight.Client.PlayedGames;

public class PlayedGamesClient
{
    private readonly HttpClient httpClient;

    public PlayedGamesClient(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("GameNight.ServerAPI");
    }

    public async Task<List<PlayedGame>> FetchAll()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/PlayedGames");

        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return (await response.Content.ReadFromJsonAsync
                <List<PlayedGame>>()) ?? new();
        }

        return new List<PlayedGame>();
    }

    public async Task<PlayedGame?> FetchById(Guid id)
    {
        var allGames = await FetchAll();
        return allGames.FirstOrDefault(x => x.Id == id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/PlayedGames/{id}");

        var response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }
}
