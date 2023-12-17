
namespace IPZ_docker.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string CarType { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Mileage { get; set; }
        public string CarStatus { get; set; } = string.Empty;

        //Navigation
        public int? PurchaseId { get; set; } = null;
        public Purchase? Purchase { get; set; }
    }
}
