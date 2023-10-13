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
		public IActionResult Get(int cartId)
		{
			var result = _cartingService.GetCartItems(cartId);
			if (result == null) {
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost]
		public IActionResult Post(Cart cart)
		{
			if (cart == null) {
				return BadRequest();
			}

			_cartingService.CreateCart(cart);
			return Ok();
		}

		[HttpPost("{cartId}")]
		public IActionResult Post(int cartId, Item item)
		{
			if (item == null) {
				return BadRequest();
			}

			_cartingService.AddItemToCart(cartId, item);
			return Ok();
		}

		[HttpDelete("{cartId}/{itemId}")]
		public IActionResult Delete(int cartId, int itemId)
		{
			_cartingService.RemoveItemFromCart(cartId, itemId);
			return Ok();
		}
	}
}