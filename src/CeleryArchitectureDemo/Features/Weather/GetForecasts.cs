using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CeleryArchitectureDemo.Features.Weather
{
    public static class GetForecasts
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public class Query : IRequest<Response>
        {
        }

        public class Response
        {
            public List<WeatherForecast> Forecast { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var rng = new Random();
                return Task.FromResult(new Response
                {
                    Forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                    {
                        DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    }).ToList()
                });
            }
        }
    }
}