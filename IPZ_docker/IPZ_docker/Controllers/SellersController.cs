using IPZ_docker.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;

namespace IPZ_docker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly IMongoCollection<Seller> _sellersCollection;

        public SellersController(IMongoDatabase database)
        {
            _sellersCollection = database.GetCollection<Seller>("SellersCollection");
        }

        // GET api/sellers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> Get()
        {
            var sellers = await _sellersCollection.Find(el => true).ToListAsync();
            return Ok(sellers);
        }

        // GET api/sellers/{id}
        [HttpGet("{id}", Name = "GetSeller")]
        public async Task<ActionResult<Seller>> Get(string id)
        {

            var objectId = ObjectId.Parse(id);
            var filter = Builders<Seller>.Filter.Eq("_id", objectId);

            var seller = await _sellersCollection.Find(filter).FirstOrDefaultAsync();

            if (seller == null)
            {
                return NotFound();
            }

            return Ok(seller);
        }

        // POST api/sellers
        [HttpPost]
        public async Task<ActionResult<Seller>> Post([FromBody] Seller seller)
        {
            seller.Id = null;
            await _sellersCollection.InsertOneAsync(seller);

            return Ok();
        }

        // PUT api/sellers/{id}
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Seller seller)
        {
            var objectId = ObjectId.Parse(seller.Id);
            var filter = Builders<Seller>.Filter.Eq("_id", objectId);

            var existingSeller = await _sellersCollection.Find(filter).FirstOrDefaultAsync();

            if (existingSeller == null)
            {
                return NotFound();
            }
            seller.Id = objectId.ToString();
            await _sellersCollection.ReplaceOneAsync(filter, seller);

            return NoContent();
        }

        // DELETE api/sellers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<Seller>.Filter.Eq("_id", objectId);

            var result = await _sellersCollection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
