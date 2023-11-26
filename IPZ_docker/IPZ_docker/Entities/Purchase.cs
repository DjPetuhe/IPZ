namespace IPZ_docker.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        //Navigation
        public Car? Car { get; set; }
        public Client? Client { get; set; }
    }
}
