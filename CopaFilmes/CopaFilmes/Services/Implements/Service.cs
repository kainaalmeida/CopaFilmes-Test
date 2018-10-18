using CopaFilmes.Models;
using CopaFilmes.Services.Abstract;
using CopaFilmes.Services.Implements;
using CopaFilmes.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Service))]
namespace CopaFilmes.Services.Implements
{
    public class Service : IService
    {
        private HttpClient _client;
        public Service()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(URLS.URL);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Filmes>> GetFilmes()
        {
            var response = await _client.GetAsync($"api/filmes");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<List<Filmes>>(content);

                return retorno.OrderBy(x=>x.titulo).ToList();
            }
            return null;
        }
    }
}
