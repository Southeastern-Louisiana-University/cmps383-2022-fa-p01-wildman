using System.Net;
using FA22.P01.Tests.Web.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FA22.P01.Tests.Web;

[TestClass]
public class WeatherForecastTests
{
    private WebTestContext context;

    [TestInitialize]
    public void Init()
    {
        context = new WebTestContext();
    }

    [TestCleanup]
    public void Cleanup()
    {
        context.Dispose();
    }

    [TestMethod]
    public async Task GetWeather_ReturnsData()
    {
        //arrange
        var webClient = context.GetStandardWebClient();

        //act
        var result = await webClient.GetAsync("weatherForecast");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = result.Content.ReadAsJsonAsync<Dto[]>();
        content.Result.Should().NotBeNull()
            .And.HaveCount(5)
            .And.AllSatisfy(x => x.Summary.Should().NotBeNullOrWhiteSpace());
    }

    internal class Dto
    {
        public int TemperatureF { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}