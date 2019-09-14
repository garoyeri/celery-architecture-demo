using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CeleryArchitectureDemo.Features.Weather
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IMediator _mediator;

        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Forecasts()
        {
            return Ok((await _mediator.Send(new GetForecasts.Query())).Forecast);
        }
    }
}
