using IPZ_docker.Database;
using IPZ_docker.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPZ_docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Purchases : ControllerBase
    {
        private readonly DataContext _dbContext;

        public Purchases (DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Purchases.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _dbContext.Purchases.FindAsync(id).AsTask());
        }

        [HttpPost]
        public async Task<IActionResult> Post(int clientId, int carId)
        {
            Client client = await _dbContext.Clients.FindAsync(clientId) ?? throw new KeyNotFoundException("Client not found.");
            Car car = await _dbContext.Cars.FindAsync(carId) ?? throw new KeyNotFoundException("Car not found.");
            if (car.PurchaseId != null) throw new ArgumentException("Car already purchased.");

            Purchase purchase = new() { Car = car, Client = client, CarId = carId, ClientId = clientId };
            await _dbContext.Purchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            car.PurchaseId = purchase.Id;
            car.Purchase = purchase;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, int clientId, int carId)
        {
            Purchase purchase = await _dbContext.Purchases.FindAsync(id) ?? throw new KeyNotFoundException("Purchase not found.");
            Client client = await _dbContext.Clients.FindAsync(clientId) ?? throw new KeyNotFoundException("Client not found.");
            Car car = await _dbContext.Cars.FindAsync(carId) ?? throw new KeyNotFoundException("Car not found.");
            if (car.PurchaseId != null && car.PurchaseId != id) throw new ArgumentException("Car already purchased.");

            purchase.Client = client;
            purchase.ClientId = clientId;
            purchase.Car = car;
            purchase.CarId = carId;
            car.PurchaseId = id;
            car.Purchase = purchase;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Purchase purchase = await _dbContext.Purchases.FindAsync(id) ?? throw new KeyNotFoundException("Purchase not found.");
            Car? car = await _dbContext.Cars.FindAsync(purchase.CarId);

            if (car != null)
            {
                car.PurchaseId = null;
                car.Purchase = null;
            }
            _dbContext.Purchases.Remove(purchase);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
