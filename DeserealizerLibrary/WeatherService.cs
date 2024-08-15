using DeserealizerLibrary.Models;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Serialization;

namespace DeserealizerLibrary
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        public string BaseUrl;
        public string City;
        public string ApiKey;

        public WeatherService(string baseUrl, string city, string apiKey)
        {
            BaseUrl = baseUrl;
            City = city;
            ApiKey = apiKey;
            _httpClient = new HttpClient();
        }


        public async Task<WeatherJsonResponse> GetWeatherJsonAsync()
        {
            try
            {
                string url = $"{BaseUrl}?q={City}&appid={ApiKey}&mode=json";
                string jsonResponse = await _httpClient.GetStringAsync(url);

                return JsonSerializer.Deserialize<WeatherJsonResponse>(jsonResponse);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<WeatherXmlResponse> GetWeatherXmlAsync()
        {
            try
            {


                string url = $"{BaseUrl}?q={City}&appid={ApiKey}&mode=xml";
                string xmlResponse = await _httpClient.GetStringAsync(url);

                var serializer = new XmlSerializer(typeof(WeatherXmlResponse));
                using (var reader = new StringReader(xmlResponse))
                {
                    return (WeatherXmlResponse)serializer.Deserialize(reader);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
