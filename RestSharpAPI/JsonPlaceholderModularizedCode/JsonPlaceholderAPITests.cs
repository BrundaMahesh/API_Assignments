using JsonPlaceholderModularizedCode.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholderModularizedCode
{
    [TestFixture]
    internal class JsonPlaceholderAPITests:CoreCodes
    {
        [Test]
        [Order(0)]
        [TestCase(1)]
        public void GetSingleUserTest(int userId)
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("GetSingleUser Test Started");

            var request = new RestRequest("posts/" + userId, Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.That(user.Id, Is.EqualTo(1));
                Log.Information("User Id matches with fetch");
                Assert.That(user.UserId, Is.EqualTo(1));
                Log.Information("User userId matches with fetch");
                Assert.IsNotEmpty(user.Title);
                Log.Information("User Title matches with fetch");
                Assert.IsNotEmpty(user.Body);
                Log.Information("User Body matches with fetch");

                test.Pass("GetSingleUserTest passed all Asserts.");
            }
            catch(AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }
        }

        [Test]
        [Order(1)]
        public void CreateUserTest()
        {
            test = extent.CreateTest("Create User");
            Log.Information("CreateUser Test Started");

            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "10", title = "RestSharp API", body = "RestSharp" });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.That(user.UserId, Is.EqualTo(10));
                Log.Information("User userId matches with fetch");

                Log.Information("CreateUser test passed all Asserts");

                test.Pass("CreateUserTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test failed");
            }
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        public void UpdateUserTest(int userId)
        {
            test = extent.CreateTest("Update User");
            Log.Information("UpdateUserTest Started");

            var request = new RestRequest("posts/" + userId, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "11", title = "Updated RestSharp API", body = "RestSharp" });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User updated and returned");

                Log.Information("UpdateUser test passed all Asserts");

                test.Pass("UpdateUserTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }
        }

        [Test]
        [Order(3)]
        [TestCase(1)]
        public void DeleteUserTest(int userId)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("DeleteUserTest Started");

            var request = new RestRequest("posts/" + userId, Method.Delete);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("User deleted");
                Log.Information("Delete User test passed");

                test.Pass("Delete User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User test failed");
            }
        }

        [Test]
        [Order(4)]
        public void GetAllUsers()
        {
            test = extent.CreateTest("Get All User");
            Log.Information("Get All User Test Started");

            var request = new RestRequest("posts", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(users);
                Log.Information("Get All User test passed");

                test.Pass("Get All User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Non-Existing User test failed");
            }
        }

        [Test]
        [Order(5)]
        [TestCase(987)]
        public void GetNonExistingUser(int userId)
        {
            test = extent.CreateTest("Get Non-Existing User");
            Log.Information("Get Non-Existing User Test Started");

            var request = new RestRequest("posts/" +userId, Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("Get Non-Existing User test passed");

                test.Pass("Get Non-Existing User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Non-Existing User test failed");
            }
        }
    }
}