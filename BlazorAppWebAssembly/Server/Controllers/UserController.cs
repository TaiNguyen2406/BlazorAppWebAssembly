using BlazorAppWebAssembly.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAppWebAssembly.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public async  Task<IEnumerable<User>> List()
        {
            IEnumerable<User> users = null;
            HttpResponseMessage response = await _client.GetAsync(
                  "/users");
            if (response.IsSuccessStatusCode)
            {
                users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(await response.Content.ReadAsStreamAsync());

            }
            return users;
        }
    }
}
