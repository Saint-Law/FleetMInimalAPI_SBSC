namespace FleetApi
{
    public class Trip
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string DriverName { get; set; }
        public string Car { get; set; }
        public double LogisticFee { get; set; }
    }
}
