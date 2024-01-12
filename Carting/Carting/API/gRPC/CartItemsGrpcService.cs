using AutoMapper;
using Carting.Core.Models.Cart;
using Carting.Services;
using Grpc.Core;

namespace Carting.API.gRPC
{
	public class CartItemsGrpcService : CartItemsService.CartItemsServiceBase //CartItemsService - name of the service in .proto
	{
		private readonly ICartingService _cartingService;
		private readonly IMapper _mapper;

		public CartItemsGrpcService(ICartingService cartingService, IMapper mapper)
		{
			_cartingService = cartingService;
			_mapper = mapper;
		}

		public override Task<CartItemsReplyMessage> GetCartItemsUnary(CartItemsRequestMessage request, ServerCallContext context)
		{
			return Task.FromResult(
					FormCartReplyMessage(request.CartId, _cartingService.GetCartItems(request.CartId))
				);
		}

		public override async Task GetCartItemsServerStreaming(CartItemsRequestMessage request, IServerStreamWriter<ItemMessage> responseStream, ServerCallContext context)
		{
			var mappedItems = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemMessage>>(
					_cartingService.GetCartItems(request.CartId)
				);
			foreach ( var item in mappedItems ) {
				await responseStream.WriteAsync( item );

				await Task.Delay(TimeSpan.FromSeconds(2));
			}
		}

		public override async Task<CartItemsReplyMessage> AddItemToCartClientStreaming(
			IAsyncStreamReader<AddItemToCartRequestMessage> requestStream,
			ServerCallContext context)
		{
			int cartId = -1;
			await foreach (AddItemToCartRequestMessage request in requestStream.ReadAllAsync()) {
				if (cartId == -1) {
					cartId = request.CartId;
				}

				if (cartId == request.CartId) {
					AddItemToCart(request.CartId, _mapper.Map<Item>(request.Item));
				}
			}

			return await Task.FromResult(
					FormCartReplyMessage(cartId, _cartingService.GetCartItems(cartId))
				);
		}

		private void AddItemToCart(int cartId, Item item)
		{
			_cartingService.AddItemToCart(cartId, item);
		}

		private CartItemsReplyMessage FormCartReplyMessage(int cartId, IEnumerable<Item> items)
		{
			var reply = new CartItemsReplyMessage() { CartId = cartId };
			var mappedItems = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemMessage>>(items);
			reply.Items.AddRange(mappedItems);
			return reply;
		}
	}
}
