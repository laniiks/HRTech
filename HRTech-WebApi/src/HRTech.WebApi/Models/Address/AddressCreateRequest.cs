namespace HRTech.WebApi.Models.Address
{
    public class AddressCreateRequest
    {
        public string Country { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}