using IPZ_docker.Database;
using IPZ_docker.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPZ_docker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Clients : ControllerBase
    {
        private readonly DataContext _dbContext;
        
        public Clients(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Clients.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _dbContext.Clients.FindAsync(id).AsTask());
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Name, int Age, string Sex)
        {
            Client client = new() { Name = Name, Age = Age, Sex = Sex };

            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, string Name, int Age, string Sex)
        {
            Client client = await _dbContext.Clients.FindAsync(id) ?? throw new KeyNotFoundException("Client not found.");

            client.Name = Name;
            client.Age = Age;
            client.Sex = Sex;

            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Client client = await _dbContext.Clients.FindAsync(id) ?? throw new KeyNotFoundException("Client not found.");

            _dbContext.Clients.Remove(client);

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
