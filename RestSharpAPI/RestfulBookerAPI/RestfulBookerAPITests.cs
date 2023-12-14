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

namespace RestfulBookerAPI
{
    [TestFixture]
    internal class RestfulBookerAPITests:CoreCodes
    {
        [Test]
        [Order(1)]
        [TestCase(3)]
        public void GetSingleBookingIdTest(int userId)
        {
            test = extent.CreateTest("Get Single Booking Id");
            Log.Information("GetSingleBookingId Test Started");

            var request = new RestRequest("/"+ userId, Method.Get);
            request.AddHeader("Accept", "application/json");
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
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


                test.Pass("GetSingleBookingIdTest passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("GetSingleBookingId test failed");
            }
        }

        [Test]
        [Order(2)]
        public void GetAllBookingIds()
        {
            test = extent.CreateTest("Get All Booking Ids");
            Log.Information("Get All Booking Ids Test Started");

            var request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
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
            Log.Information("CreateBooking Test Started");

            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new {
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
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
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

                Log.Information("CreateBooking test passed all Asserts");

                test.Pass("CreateBooking passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create Booking test failed");
            }
        }

        [Test]
        [Order(4)]
        [TestCase(1)]
        public void DeleteBookingTest(int userId)
        {
            test = extent.CreateTest("Delete Booking");
            Log.Information("DeleteBookingTest Started");

            var request = new RestRequest("/" + userId, Method.Delete);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Forbidden));
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
        [Order(5)]
        public void CreateAuthTokenTest()
        {
            test = extent.CreateTest("Auth User Test");
            var request = new RestRequest("/auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { username = "admin", password = "password123" });

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var user = JsonConvert.DeserializeObject<Cookies>(response.Content);

                Assert.NotNull(user);
                Log.Information("Get UserData Test Passed");
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
    }
}
