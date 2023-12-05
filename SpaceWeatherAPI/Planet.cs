namespace SpaceWeatherAPI
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Satellite> Satellites { get; set; }
        public int WeatherForecast { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
