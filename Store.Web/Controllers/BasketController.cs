using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServices;
using Store.Service.Services.BasketServices.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string id)
            => Ok(await _basketService.GetBasketAsync(id));
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto customerBasketDto)
            => Ok(await _basketService.UpdateBasketAsync(customerBasketDto));

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasketAsync(string id)
            => Ok(await _basketService.DeleteBasketAsync(id));
    }
}
