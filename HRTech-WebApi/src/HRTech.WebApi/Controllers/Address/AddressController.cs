using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Address.Contracts;
using HRTech.Application.Services.Address.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddressInCompany(EditAddress address, CancellationToken cancellationToken)
        {
            var result = await _addressService.UpdateAddressInCompany(new Edit.Request
            {
                Id = address.Id,
                CompanyAddress = new Edit.Request.Address
                {
                    Country = address.Country,
                    City = address.City,
                    Street = address.Street,
                    HouseNumber = address.HouseNumber
                }
            }, cancellationToken);
            return Ok(result);
        }
    }

    public class EditAddress
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}