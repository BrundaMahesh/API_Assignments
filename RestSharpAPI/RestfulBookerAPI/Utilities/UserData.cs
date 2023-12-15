using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBookerAPI.Utilities
{
    public class UserData
    {
        [JsonProperty("firstname")]
        public string? FirstName { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("totalprice")]
        public string? TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public string? Depositpaid { get; set; }

        [JsonProperty("additionalneeds")]
        public string? AdditionalNeeds { get; set; }

        [JsonProperty("bookingdates")]
        public BookingDates BookingDates { get; set; }

    }
    public class BookingDates
    {
        [JsonProperty("checkin")]
        public string? CheckIn { get; set; }

        [JsonProperty("checkout")]
        public string? CheckOut { get; set; }
    }
    public class CreateBooking
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public UserData Booking { get; set; }

    }
}
