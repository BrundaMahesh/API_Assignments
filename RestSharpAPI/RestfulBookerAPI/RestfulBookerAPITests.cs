using JsonPlaceholderModularizedCode;
using Newtonsoft.Json;
using RestfulBookerAPI.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBookerAPI
{
    [TestFixture]
    internal class RestfulBookerAPITests:CoreCodes
    {
        [Test]
        [Order(0)]
        [TestCase(3)]
        public void GetSingleBookingIdTest(int userId)
        {
            test = extent.CreateTest("Get Single Booking Id");
            Log.Information("GetSingleBookingId Test Started");

            var request = new RestRequest(""+ userId, Method.Get);
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
                Assert.That(user.TotalPrice, Is.EqualTo(573));
                Log.Information("User TotalPrice matches with fetch");
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
        [Order(1)]
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
    }
}
//////