using Newtonsoft.Json;
using RestfulBookerAPI.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBookerAPI.TestScripts
{
    [TestFixture]
    internal class RestfulBookerAPITests : CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase(9)]
        public void GetSingleBookingIdTest(int userId)
        {
            test = extent.CreateTest("Get Single Booking Id");
            Log.Information("Get Single Booking Id Test Started");

            var request = new RestRequest("booking/" + userId, Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);

                Assert.NotNull(user);
                Log.Information("User returned");
                Assert.IsNotEmpty(user.FirstName);
                Log.Information("User FirstName matches with fetch");
                Assert.IsNotEmpty(user.LastName);
                Log.Information("User lastName matches with fetch");
                Assert.IsNotEmpty(user.Depositpaid);
                Log.Information("User DepositPaid matches with fetch");
                
                Log.Information("Get Single Booking Id test passed");

                test.Pass("Get Single Booking Id Test passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Get Single Booking Id test failed");
            }
        }

        [Test]
        [Order(2)]
        public void GetAllBookingIdsTest()
        {
            test = extent.CreateTest("Get All Booking Ids");
            Log.Information("Get All Booking Ids Test Started");

            var request = new RestRequest("booking", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(users);
                Log.Information("Get All Booking Ids test passed");

                test.Pass("Get All Booking Ids test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get All Booking Ids test failed");
            }
        }

        [Test]
        [Order(3)]
        public void CreateBookingTest()
        {
            test = extent.CreateTest("Create Booking");
            Log.Information("Create Booking Test Started");

            var request = new RestRequest("booking", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new
            {
                firstname = "John",
                lastname = "Doe",
                totalprice = "200",
                depositpaid = "true",
                bookingdates = new
                {
                    checkin = "2023-03-01",
                    checkout = "2023-03-15"
                },
                additionalneeds = "Extra pillows"
            });

            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.IsNotEmpty(user.FirstName);
                Log.Information("User First Name matches with fetch");
                Assert.IsNotEmpty(user.LastName);
                Log.Information("User Last Name matches with fetch");
                Assert.IsNotEmpty(user.TotalPrice);
                Log.Information("User Total Price matches with fetch");
                Assert.IsNotEmpty(user.Depositpaid);
                Log.Information("User Depositpaid matches with fetch");
                Assert.IsNotEmpty(user.CheckIn);
                Log.Information("User CheckIn matches with fetch");
                Assert.IsNotEmpty(user.CheckOut);
                Log.Information("User CheckOut matches with fetch");
                Assert.IsNotEmpty(user.AdditionalNeeds);
                Log.Information("User AdditionalNeeds matches with fetch");

                Log.Information("Create Booking test passed all Asserts");

                test.Pass("Create Booking passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create Booking test failed");
            }
        }

        [Test]
        [Order(4)]
        [TestCase(3)]
        public void UpdateBookingTest(int userId)
        {
            test = extent.CreateTest("Update Booking ");
            Log.Information("Update Booking Test Started");

            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Assert.NotNull(user);
                Log.Information("Update User test passed");

                var reqput = new RestRequest("booking/" + userId, Method.Put);
                reqput.AddHeader("Content-Type", "Application/Json");
                reqput.AddHeader("Cookie", "token=" + user.Token);
                reqput.AddJsonBody(new
                {
                    firstname = "Updated John",
                    lastname = "Updated Doe",
                    totalprice = "200",
                    depositpaid = "true",
                    bookingdates = new
                    {
                        checkin = "2023-03-01",
                        checkout = "2023-03-15"
                    },
                    additionalneeds = "Extra pillows"
                });

                test.Pass("Update User Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Update Booking test failed");
            }
        }
        [Test]
        [Order(5)]
        [TestCase(10)]
        public void DeleteBookingTest(int userId)
        {
            test = extent.CreateTest("Delete Booking");
            Log.Information("Delete Booking Test Started");

            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);

            try
            {
                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);
                var req = new RestRequest("booking/" + userId, Method.Delete);
                req.AddHeader("Content-Type", "Application/Json");
                req.AddHeader("Cookie", "token=" + user.Token);

                var res = client.Execute(req);

                Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Log.Information($"API Error: {res.Content}");
                Log.Information("User deleted");

                Log.Information("Delete Booking test passed");

                test.Pass("Delete Booking test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Delete Booking test failed");
            }
        }

        [Test]
        [Order(6)]
        public void CreateAuthTokenTest()
        {
            test = extent.CreateTest("Create Auth Token");
            var request = new RestRequest("/auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { username = "admin", password = "password123" });

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);

                Assert.NotNull(user);
                Log.Information("User Token Test Passed");
                Console.WriteLine(user.Token);
                Assert.IsNotEmpty(user.Token);
                Log.Information("User Token matches with fetch");

                test.Pass("Create Auth Token Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Create Auth Token Test Fail");
            }
        }
        [Test]
        [Order(7)]
        [TestCase(3)]
        public void PartialUpdateBookingTest(int userId)
        {
            test = extent.CreateTest("Partial Update Booking ");
            Log.Information("Partial Update Booking Test Started");

            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "Application/Json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);
                Assert.NotNull(user);
                Log.Information("Partial Update Booking test passed");

                var reqput = new RestRequest("booking/" + userId, Method.Put);
                reqput.AddHeader("Content-Type", "Application/Json");
                reqput.AddHeader("Cookie", "token=" + user.Token);
                reqput.AddJsonBody(new
                {
                    totalprice = "500",
                    additionalneeds = "Extra pillows and Breakfast"
                });

                Log.Information("Partial Update Booking test passed");

                test.Pass("Partial Update Booking Test Passed");
            }
            catch (AssertionException)
            {
                test.Fail("Partial Update Booking test failed");
            }
        }

        [Test]
        [Order(8)]
        public void GetHealthCheckTest()
        {
            test = extent.CreateTest("Get Health Check");
            Log.Information("Get Health Check Test Started");

            var request = new RestRequest("ping", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                Log.Information("Get Health Check test passed");

                test.Pass("Get Health Check test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get Health Check test failed");
            }
        }
    }
}
