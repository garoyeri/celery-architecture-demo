namespace CeleryArchitectureTests
{
    using System.Threading.Tasks;
    using CeleryArchitectureDemo.Features.Weather;
    using Shouldly;
    using static Testing;

    public class WeatherTests
    {
        public async Task CanGetForecasts()
        {
            var response = await Send(new GetForecasts.Query());
            response.Forecast.ShouldNotBeEmpty();
            response.Forecast[0].DateFormatted.ShouldNotBeNull();
        }
    }
}