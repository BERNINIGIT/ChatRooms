using ChatRooms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatRooms.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var user = HttpContext.User;
            var identity = _httpContextAccessor.HttpContext.User.Identity.Name;

            //var userId = _httpContextAccessor.HttpContext.User;
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ar userName = User.FindFirstValue(ClaimTypes.Name);
            var user2 = await _userManager.FindByIdAsync(id);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
