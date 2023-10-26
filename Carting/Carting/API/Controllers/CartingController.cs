using Carting.Core.Models.Cart;
using Carting.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carting.API.Controllers
{
	[ApiController]
	[Route("carts")]
	public class CartingController : ControllerBase
	{
		private readonly ICartingService _cartingService;

		public CartingController(ICartingService cartingService)
		{
			_cartingService = cartingService;
		}

		/// <summary>
		/// Receives cart information
		/// </summary>
		/// <param name="id">ID of the cart to get</param>
		/// <response code="200">The item was found</response>
		/// <response code="404">The item was not found</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("{cartId}")]
		public IActionResult Get(int cartId)
		{
			var result = _cartingService.GetCart(cartId);
			if (result == null) {
				return NotFound();
			}
			return Ok(result);
		}

		/// <summary>
		/// Creates a cart
		/// </summary>
		/// <param name="cart">new cart to create</param>
		/// <response code="201">The item was created</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		public IActionResult Post(Cart cart)
		{
			_cartingService.CreateCart(cart);
			return CreatedAtAction(nameof(Get), routeValues: new { id = cart.Id }, cart);
		}

		/// <summary>
		/// Creates an item for a cart by a key. If there was no cart for specified key – creates it.
		/// </summary>
		/// <param name="cartId">cart ID</param>
		/// <param name="item">new item to create</param>
		/// <response code="201">The item was created</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost("{cartId}")]
		public IActionResult PostItem(int cartId, Item item)
		{
			_cartingService.AddItemToCart(cartId, item);
			return CreatedAtAction(nameof(PostItem), item);
		}

		/// <summary>
		/// Delete item from cart
		/// </summary>
		/// <param name="cartId">cart ID</param>
		/// <param name="itemId">ID of an item to delete</param>
		/// <response code="201">The item was deleted</response>
		/// <response code="404">An item having specified item ID was not found</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{cartId}/{itemId}")]
		public IActionResult Delete(int cartId, int itemId)
		{
			var deletedItem = _cartingService.RemoveItemFromCart(cartId, itemId);
			if(deletedItem == null) {
				return NotFound();
			}

			return Ok(deletedItem);
		}
	}
}