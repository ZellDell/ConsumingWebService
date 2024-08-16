using DeserealizerLibrary;

namespace ConsumingWebService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
            const string ApiKey = "<YOUR API KEY HERE>";


            Console.WriteLine("WEATHER SERVICE");
            Console.WriteLine("********************\n");

            Console.WriteLine("Name of the City: ");
            string City = Console.ReadLine();



            WeatherService weatherService = new(BaseUrl, City, ApiKey);

            var jsonWeather = await weatherService.GetWeatherJsonAsync();

            var XmlWeather = await weatherService.GetWeatherXmlAsync();

            if (jsonWeather == null || XmlWeather == null)
            {
                Console.WriteLine("City not Found!");
                return;
            }

            Console.WriteLine("********************\n");
            Console.WriteLine($"JSON DESERIALIZE\n");

            Console.WriteLine($"City: {jsonWeather.Name}, {jsonWeather.Sys.Country}");
            Console.WriteLine($"Temperature : {jsonWeather.Main.Temp} | {jsonWeather.Weather[0].Description}");



            Console.WriteLine($"XML DESERIALIZE\n");

            Console.WriteLine($"City: {XmlWeather.City.Name}, {XmlWeather.City.Country}");
            Console.WriteLine($"Temperature : {XmlWeather.Temperature.Value} {XmlWeather.Temperature.Unit}| {XmlWeather.Clouds.Name}");

            Console.WriteLine();

        }
    }
}
