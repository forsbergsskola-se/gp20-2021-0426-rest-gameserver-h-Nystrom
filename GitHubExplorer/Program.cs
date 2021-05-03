using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubExplorer
{
    class Program
    {
        static void Main(string[] args){

            var hello = GetHtmlFromWebsite("https://www.Github.com");
            hello.Wait();
            
            
            
            Console.WriteLine("Press any key to quit!");
            Console.ReadKey();
        }

        static async Task GetHtmlFromWebsite(string url){
            var httpClient = new HttpClient();
            try{
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e){
                Console.WriteLine(e);
            }
            httpClient.Dispose();
        }
    }
}
