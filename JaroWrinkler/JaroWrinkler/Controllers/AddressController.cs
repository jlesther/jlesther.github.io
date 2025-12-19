using JaroWrinklerSimilarity.Models;
using JaroWrinklerSimilarity.Models.Responses;
using JaroWrinklerSimilarity.Services;
using Microsoft.AspNetCore.Mvc;

namespace JaroWrinklerSimilarity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressMatchingService _addressMatchingService;

        public AddressController(IAddressMatchingService addressMatchingService)
        {
            _addressMatchingService = addressMatchingService;
        }

        [HttpPost("match-address")]
        public async Task<ActionResult<AddressMatchResponse>> MatchAddress(
            [FromBody] AddressInput input)
        {
            if (input == null)
            {
                return BadRequest("Address input is required.");
            }

            var result = await _addressMatchingService.MatchAddressAsync(input);

            if (result.MatchType == Models.Enums.AddressMatchType.None)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}