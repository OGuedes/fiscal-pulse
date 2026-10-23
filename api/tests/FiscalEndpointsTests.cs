using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FiscalPulse.Api.Tests;
public sealed class FiscalEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public FiscalEndpointsTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();
    [Fact] public async Task Dashboard_returns_next_due() => Assert.Equal(HttpStatusCode.OK, (await _client.GetAsync("/api/dashboard")).StatusCode);
    [Fact] public async Task Unknown_obligation_returns_not_found() => Assert.Equal(HttpStatusCode.NotFound, (await _client.PatchAsJsonAsync($"/api/obligations/{Guid.NewGuid()}/complete", new { documentName = "guia.pdf" })).StatusCode);
}
