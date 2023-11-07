namespace webDefAPI.Services
{
    public static class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        static List<WeatherForecast> Weathers {  get; }
        static int nextId = 3;
        static WeatherForecastService()
        {
            Weathers = new List<WeatherForecast>
            {
                new WeatherForecast{Id=1, Date=DateTime.Today, TemperatureC = 32,Summary=Summaries[Random.Shared.Next(Summaries.Length)]},
                new WeatherForecast{Id=2, Date=DateTime.Today, TemperatureC = 15,Summary=Summaries[Random.Shared.Next(Summaries.Length)]}
            };
        }
        public static List<WeatherForecast> GetAll() => Weathers;
        public static WeatherForecast? Get(int id) => Weathers.FirstOrDefault(p => p.Id == id);
        public static void Add(WeatherForecast weather)
        {
            weather.Id = nextId++;
            Weathers.Add(weather);
        }
        public static void Update(WeatherForecast weather)
        {
            var index = Weathers.FindIndex(w => w.Id == weather.Id);
            if(index == -1)
            {
                return;
            }
            Weathers[index] = weather;
        }
        public static void Delete(int id)
        {
            var weather = Get(id);
            if(weather is null)
            {
                return;
            }
            Weathers.Remove(weather);
        }
        public static List<WeatherForecast> GetBySummary(string summary)
        {
            return Weathers.Where(w => w.Summary == summary).ToList();
        }

    }
}
