using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppReadyGo.API.Models;
using System.Net;

namespace API.Client.Test
{
    class Program
    {

        static readonly Uri _baseAddress = new Uri("http://localhost:63321/");

        static void Main(string[] args)
        {

            HttpClient client = new HttpClient();

            var user = new { id = "user1", pass = "1234" };

            // Post contact
            Uri address = new Uri(_baseAddress, "/api/readygomarket/");

            //HttpResponseMessage response =  await client.GetAsync(address);

            // Check that response was successful or throw exception
            //response.EnsureSuccessStatusCode();



        }
    }
}
