using Carting.Core.Models.Cart;
using Carting.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carting.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CartingController : ControllerBase
	{
		private readonly ICartingService _cartingService;

		public CartingController(ICartingService cartingService)
		{
			_cartingService = cartingService;
		}

		[HttpGet("{cartId}")]
		public async Task<IActionResult> Get(int cartId)
		{
			var result = await _cartingService.GetCartItemsAsync(cartId);
			if (result == null) {
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post(Cart cart)
		{
			if (cart == null) {
				return BadRequest();
			}

			await _cartingService.CreateCartAsync(cart);
			return Ok();
		}

		[HttpPost("{cartId}")]
		public async Task<IActionResult> Post(int cartId, Item item)
		{
			if (item == null) {
				return BadRequest();
			}

			await _cartingService.AddItemToCartAsync(cartId, item);
			return Ok();
		}

		[HttpDelete("{cartId}/{itemId}")]
		public async Task<IActionResult> Delete(int cartId, int itemId)
		{
			await _cartingService.RemoveItemFromCartAsync(cartId, itemId);
			return Ok();
		}
	}
}