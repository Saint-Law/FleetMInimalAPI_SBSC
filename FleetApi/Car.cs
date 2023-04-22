using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FleetApi
{
    public class Car 
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string EngineNumber { get; set; }
        public string Status { get; set; }

    }

}
