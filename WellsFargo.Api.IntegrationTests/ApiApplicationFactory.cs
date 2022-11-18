using Microsoft.AspNetCore.Mvc.Testing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellsFargo.Api.IntegrationTests
{
    public static class ApiApplicationFactory
    {
        public static HttpClient CreateClient()
        {
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {

                builder.ConfigureServices(s =>
                {

                });
            });

            var client = factory.CreateClient();
            return client;
        }
    }
}
