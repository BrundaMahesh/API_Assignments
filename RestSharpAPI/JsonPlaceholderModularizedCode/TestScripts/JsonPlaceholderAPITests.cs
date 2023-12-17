using JsonPlaceholderModularizedCode.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholderModularizedCode.TestScripts
{
    [TestFixture]
    internal class JsonPlaceholderAPITests : CoreCodes
    {
        [Test]
        [Order(1)]
        public void GetAllUsersTest()
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
                test.Fail("Get All User test failed");
            }
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        public void GetSingleUserTest(int userId)
        {
            test = extent.CreateTest("Get Single User");
            Log.Information("Get Single User Test Started");

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

                Log.Information("Get Single User test passed");
                test.Pass("Get Single User Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Single User test failed");
            }
        }

        [Test]
        [Order(3)]
        [TestCase(1)]
        public void GetSingleUserCommentsTest(int userId)
        {
            test = extent.CreateTest("Get Single User Comments");
            Log.Information("Get Single User Comments Test Started");

            var request = new RestRequest("posts/" + userId + "/comments", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<UserData> user = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");

                Log.Information("Get Single User Comments test passed");

                test.Pass("Get Single User Comments Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Single User Comments test failed");
            }
        }

        [Test]
        [Order(4)]
        [TestCase(1)]
        public void GetCommentsOfParticularPostIdTest(int userId)
        {
            test = extent.CreateTest("Get Comments Of Particular PostId");
            Log.Information("Get Comments Of Particular PostId Test Started");

            var request = new RestRequest("comments?postId=" + userId, Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<UserData> user = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");

                Log.Information("Get Comments Of Particular PostId test passed");

                test.Pass("Get Comments Of Particular PostId Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Comments Of Particular PostId test failed");
            }
        }
        [Test]
        [Order(5)]
        public void CreateUserTest()
        {
            test = extent.CreateTest("Create User");
            Log.Information("Create User Test Started");

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
                Log.Information($"User userId matches with fetch {user.UserId}");
                Assert.That(user.Title, Is.EqualTo("RestSharp API"));
                Log.Information($"User Title matches with fetch {user.Title}");
                Assert.That(user.Body, Is.EqualTo("RestSharp"));
                Log.Information($"User Body matches with fetch {user.Body}");

                Log.Information("Create User test passed");

                test.Pass("Create User Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test failed");
            }
        }

        [Test]
        [Order(6)]
        [TestCase(1)]
        public void UpdateUserTest(int userId)
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User Test Started");

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

                Log.Information("Update User test passed");

                test.Pass("Update User Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }
        }

        [Test]
        [Order(7)]
        [TestCase(1)]
        public void PatchUserTest(int userId)
        {
            test = extent.CreateTest("Patch User");
            Log.Information("Patch User Test Started");

            var request = new RestRequest("posts/" + userId, Method.Patch);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { body = "RestSharp for C#" });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User updated and returned");

                Log.Information("Patch User test passed");

                test.Pass("Patch User Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Patch User test failed");
            }
        }
        [Test]
        [Order(8)]
        [TestCase(1)]
        public void DeleteUserTest(int userId)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("Delete User Test Started");

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
        [Order(9)]
        [TestCase(987)]
        public void GetNonExistingUser(int userId)
        {
            test = extent.CreateTest("Get Non-Existing User");
            Log.Information("Get Non-Existing User Test Started");

            var request = new RestRequest("posts/" + userId, Method.Get);
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