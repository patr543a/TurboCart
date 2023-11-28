using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;

namespace TurboCart.Infrastructure.Networking.Services;

public class ServiceBase([StringSyntax("Uri")] string _baseUrl)
{
    protected async Task<T?> GetFromJsonAsync<T>([StringSyntax("Uri")] string url)
    {
        using var client = GetHttpClient();

        return await client.GetFromJsonAsync<T>(url);
    }

    protected async Task<T?> PostAsJsonAsync<T>([StringSyntax("Uri")] string url, T value)
    {
        using var client = GetHttpClient();

        var response = await client.PostAsJsonAsync(url, value);

        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> PutAsJsonAsync<T>([StringSyntax("Uri")] string url, T value)
    {
        using var client = GetHttpClient();

        var response = await client.PutAsJsonAsync(url, value);

        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> DeleteFromJsonAsync<T>([StringSyntax("Uri")] string url)
    {
        using var client = GetHttpClient();

        return await client.DeleteFromJsonAsync<T>(url);
    }

    private HttpClient GetHttpClient()
        => new() { BaseAddress = new(_baseUrl) };
}