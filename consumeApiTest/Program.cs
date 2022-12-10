using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace consumeApiTest
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Complaint comp)
        {
            Console.WriteLine($"Name: {comp.Name}\tBody: " +
                $"{comp.Body}\tCreated: {comp.Created}");
        }

        static async Task<Uri> CreateProductAsync(Complaint comp)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/api/CompItems", comp);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Complaint> GetProductAsync(string path)
        {
            Complaint comp = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                comp = await response.Content.ReadAsAsync<Complaint>();
            }
            return comp;
        }
        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"/api/CompItems/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:7263");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Complaint comp = new Complaint
                {
                    Name = "Adam",
                    Body = "Somebody didn't do their washing",
                    Title = "Washing problem on 12/9/22"
                };

                var url = await CreateProductAsync(comp);
                Console.WriteLine($"Created at {url}");

                // Get the product
                comp = await GetProductAsync(url.PathAndQuery);
                ShowProduct(comp);

                //// Get the updated product
                //comp = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(comp);

                //Delete the product
                var statusCode = await DeleteProductAsync(comp.Id.ToString());
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}