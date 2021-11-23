using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCar.Context;
using MyCar.Models;

namespace MyCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller {
        private readonly AppDbContext _appDbcontext;

        public CarsController(AppDbContext appDbContext) {
            _appDbcontext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars() {
            return Ok(new
            {
                success = true,
                data = await _appDbcontext.Cars.ToListAsync()
            });
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Car>> GetCarById(int? id) {
            var car = await _appDbcontext.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
                return car;
        }

        [HttpPut]

        public async Task<IActionResult> CreateCar(Car car) {
            _appDbcontext.Cars.Add(car);
            _appDbcontext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = car
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id) {
            var car = await _appDbcontext.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            _appDbcontext.Cars.Remove(car);
            _appDbcontext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = car
            });
        }
        
    }
}
