using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using TermInfoByTermId.Models;

namespace TermInfoByTermId.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public string Terms(string username, string password, int id)

        {

            string url = $"https://sisclientweb-700031.campusnexus.cloud/ds/odata/Terms?$filter=Id eq {id}";

            string auth = $"{username}:{password}";
            var bytes = Encoding.UTF8.GetBytes(auth);
            var base64 = Convert.ToBase64String(bytes);

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);

                var postTask = client.GetAsync(url).GetAwaiter().GetResult();

                if (postTask.IsSuccessStatusCode)
                {
                    var result = postTask.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return result;
                }
                else
                {
                    // Handle unsuccessful response status code
                    // For example, throw an exception or return an error message
                    throw new HttpRequestException($"HTTP request failed with status code {postTask.StatusCode}");
                }

            }


        }
    }
}
