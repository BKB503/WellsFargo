using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Net;

namespace WellsFargo.Api.IntegrationTests.OrderManagementSystemApi
{
    public class Tests
    {
        private readonly string apiUrl = $"/api/OrderManagementSystem/DownloadOutput";

        [Test]
        public async Task WhenDownloadOutputApiCall_ThenMustReturnBadRequest()
        {
            //set data
            var client = ApiApplicationFactory.CreateClient();
            var filePath = $"{Directory.GetCurrentDirectory()}\\CsvFiles\\transactions.csv";
            using var content = new MultipartFormDataContent();
               var fileInfo = new FileInfo(filePath);
            using var fileStream = fileInfo.OpenRead();
            var fileContent = new StreamContent(fileStream);
            content.Add(fileContent, "file", "transactions.csv");

            //call api

            var responseMessage = await client.PostAsync(apiUrl, content);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            //assert response
            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseContent, Is.EqualTo("Please upload a valid csv file"));
        }

        [Test]
        public async Task WhenDownloadOutputApiCall_ThenMustReturnOKResultWithMediaTypeAsZip()
        {
            //set data
            var client = ApiApplicationFactory.CreateClient();
            var filePath = $"{Directory.GetCurrentDirectory()}\\CsvFiles\\transactions.csv";
            using var content = new MultipartFormDataContent();
            
            var fileInfo = new FileInfo(filePath);
            using var fileStream = fileInfo.OpenRead();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/csv");
            content.Add(fileContent, "file", "transactions.csv");

            
            var responseMessage = await client.PostAsync(apiUrl, content);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var ttt = responseMessage.Content.Headers.ContentType;

            //assert response
            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseMessage.Content.Headers.ContentType.MediaType, Is.EqualTo("application/zip"));
        }
    }
}