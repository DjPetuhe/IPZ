namespace IPZ_docker.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        //Navigation
        public int CarId { get; set; }
        public Car Car { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
