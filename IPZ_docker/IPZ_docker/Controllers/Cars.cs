using IPZ_docker.Database;
using IPZ_docker.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPZ_docker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Cars : ControllerBase
    {
        private readonly DataContext _dbContext;

        public Cars(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Cars.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _dbContext.Cars.FindAsync(id).AsTask());
        }

        [HttpPost]
        public async Task<IActionResult> Post(string CarType, double Price, double Mileage, string CarStatus)
        {
            Car car = new() { CarType = CarType, Price = Price, Mileage = Mileage, CarStatus = CarStatus };

            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, string CarType, double Price, double Mileage, string CarStatus)
        {
            Car car = await _dbContext.Cars.FindAsync(id).AsTask() ?? throw new KeyNotFoundException("Car not found.");

            car.CarType = CarType;
            car.Price = Price;
            car.Mileage = Mileage;
            car.CarStatus = CarStatus;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Car car = await _dbContext.Cars.FindAsync(id).AsTask() ?? throw new KeyNotFoundException("Car not found.");

            _dbContext.Cars.Remove(car);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
