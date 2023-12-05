using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SpaceWeatherAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static List<Planet> PlanetList = new List<Planet>()
        {
            new Planet
            {
                Id = 1,
                Name = "Mercurry",
                WeatherForecast = 420,
                  Satellites = new List<Satellite>
                  {}
            },
             new Planet
            {
                Id = 2,
                Name = "Mars",
                WeatherForecast = -63,
                 Satellites = new List<Satellite>
                  {
                        new Satellite { Id = 201, Name = "Phobos", WeatherForecast= -124 },
                        new Satellite { Id = 202, Name = "Deimos", WeatherForecast = -185 }
                  }
            },
              new Planet
            {
                Id = 3,
                Name = "Earth",
                WeatherForecast = 12,
                 Satellites = new List<Satellite>
                  {
                        new Satellite { Id = 301, Name = "Moon", WeatherForecast = 95 },

                  },

            },
        };



        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string sort = "")
        {
            // Filtering
            var filteredList = PlanetList;

            if (!string.IsNullOrEmpty(sort))
            {

                if (sort.ToLower() == "created_date")
                {
                    filteredList = filteredList.OrderBy(p => p.CreatedDate).ToList();
                }
            }

            // Pagination
            var paginatedList = PaginateList(filteredList, page, size);


            var response = new
            {
                TotalCount = PlanetList.Count,
                TotalPages = (int)Math.Ceiling((double)PlanetList.Count / size),
                Page = page,
                Size = size,
                Data = paginatedList
            };

            return Ok(response);
        }

        [HttpGet("(id)")]

        public Planet GetById(int id)
        {
            var planet = PlanetList.Where(Planet => Planet.Id == id).SingleOrDefault();
            return planet;
        }



        [HttpPost]
        public IActionResult Post(Planet planet)
        {

            var resourceUri = $"/api/WeatherForecast/{planet.Id}";

            return Created(resourceUri, planet);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Planet updatePlanet)
        {
            var planet = PlanetList.SingleOrDefault(x => x.Id == id);

            if (planet == null & updatePlanet.Id != id)
            {
                return BadRequest();
            }


            planet.Name = updatePlanet.Name != default ? updatePlanet.Name : planet.Name;


            return Ok(planet);
        }


        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] Planet partialUpdate)
        {
            var planet = PlanetList.SingleOrDefault(x => x.Id == id);

            if (planet == null)
            {
                return BadRequest();
            }

            if (partialUpdate.Name != default)
            {
                planet.Name = partialUpdate.Name;
            }

            return Ok(planet);
        }



        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            var planet = PlanetList.SingleOrDefault(x => x.Id == id);
            if (planet == null)
            {
                return BadRequest();
            }
            PlanetList.Remove(planet);
            return NoContent();
        }


        private List<Planet> ApplySorting(List<Planet> list, string sort)
        {
            switch (sort.ToLower())
            {
                case "created_date":
                    return list.OrderBy(p => p.CreatedDate).ToList();
                default:
                    return list;
            }
        }


        private List<Planet> PaginateList(List<Planet> list, int page, int size)
        {
            return list.Skip((page - 1) * size).Take(size).ToList();
        }
   
    }

}
