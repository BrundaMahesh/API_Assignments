using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholderAPINUnit
{
    [TestFixture]
    internal class JsonPlaceholderAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }

        [Test]
        [Order(0)]
        public void GetSingleUserTest()
        {
            var request = new RestRequest("posts/1", Method.Get);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);

            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(1));
            Assert.That(user.UserId,Is.EqualTo(1));
            Assert.IsNotEmpty(user.Title);
            Assert.IsNotEmpty(user.Body);
        }

        [Test]
        [Order(1)]
        public void CreateUserTest()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "10", title = "RestSharp API", body = "RestSharp" });

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
        }

        [Test]
        [Order(2)]
        public void UpdateUserTest()
        {
            var request = new RestRequest("posts/1", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "11", title = "Updated RestSharp API", body = "RestSharp" });

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
        }

        [Test]
        [Order(3)]
        public void DeleteUserTest()
        {
            var request = new RestRequest("posts/1", Method.Delete);
            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
    }
}
