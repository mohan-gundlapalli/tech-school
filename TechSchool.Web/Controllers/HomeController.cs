using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechSchool.Web.DTO;
using TechSchool.Web.Models;
using TechSchool.Web.Repositories;

namespace TechSchool.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISchoolRepository _repo;

        public HomeController(ILogger<HomeController> logger, ISchoolRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Cities/{contactCount?}")]
        public async Task<IActionResult> Cities(int? contactCount)
        {
            CityQueryDTO query = null;

            if (contactCount.HasValue)
            {
                query = new()
                {
                    Name = "",
                    StudentCount = contactCount ?? 0
                };
            }
            var cities = await _repo.GetCities(query);


            return View(cities);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
