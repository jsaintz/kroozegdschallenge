using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Krooze.EntranceTest.WriteHere.Structure.Services
{
    public class MoviesService
    {
        const string API_MOVIES = "https://swapi.co/api/films/";
        private string responseResult = string.Empty;

        public MoviesService()
        {
            if (GetAllMovies == null)
                SetMoviesResponse();
        }

        private void SetMoviesResponse()
        {
            using (var request = new HttpClient())
            {
                Task<HttpResponseMessage> response = request.GetAsync(API_MOVIES);
                if (response.Result.IsSuccessStatusCode)
                {
                    responseResult = response.Result.Content.ReadAsStringAsync().Result;
                }
                GetAllMovies = JObject.Parse(responseResult);
            }
        }

        public JObject GetAllMovies { get; private set; } = null;

        public string GetDirector
        {
            get => GetAllMovies.SelectToken("results[3].director").ToString();
        }
    }
}
