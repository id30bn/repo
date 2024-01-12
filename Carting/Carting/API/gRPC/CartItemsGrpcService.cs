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
			var reply = new CartItemsReplyMessage() { CartId = request.CartId };
			var mappedItems = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemMessage>>(
				_cartingService.GetCartItems(request.CartId)
				);
			reply.Items.AddRange(mappedItems);

			return Task.FromResult(reply);
		}

		public override async Task GetCartItemsServerStreaming(CartItemsRequestMessage request, IServerStreamWriter<ItemMessage> responseStream, ServerCallContext context)
		{
			var mappedItems = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemMessage>>(
					_cartingService.GetCartItems(request.CartId)
				);
			foreach ( var item in mappedItems ) {
				await responseStream.WriteAsync( item );

				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}

		public override async Task<AddItemsReplyMessage> AddItemToCartClientStreaming(
			IAsyncStreamReader<ItemMessage> requestStream,
			ServerCallContext context)
		{
			int count = 0;
			await foreach (var itemMessage in requestStream.ReadAllAsync()) {
				AddItemToCart(itemMessage.CartId, _mapper.Map<Item>(itemMessage));
				count++;
			}

			return new AddItemsReplyMessage { AddedItemsCount = count };
		}

		public override async Task AddItemToCartBiDirectionalStreaming(IAsyncStreamReader<ItemMessage> requestStream, IServerStreamWriter<ItemMessage> responseStream, ServerCallContext context)
		{
			await foreach (var itemMessage in requestStream.ReadAllAsync()) {
				AddItemToCart(itemMessage.CartId, _mapper.Map<Item>(itemMessage));
				await responseStream.WriteAsync(itemMessage);
			}
		}

		//public override async Task<CartItemsReplyMessage> AddItemToCartClientStreaming(
		//	IAsyncStreamReader<AddItemToCartRequestMessage> requestStream,
		//	ServerCallContext context)
		//{
		//	int cartId = -1;
		//	await foreach (AddItemToCartRequestMessage request in requestStream.ReadAllAsync()) {
		//		if (cartId == -1) {
		//			cartId = request.CartId;
		//		}

		//		if (cartId == request.CartId) {
		//			AddItemToCart(request.CartId, _mapper.Map<Item>(request.Item));
		//		}
		//	}

		//	return await Task.FromResult(
		//			FormCartReplyMessage(cartId, _cartingService.GetCartItems(cartId))
		//		);
		//}

		private void AddItemToCart(int cartId, Item item)
		{
			_cartingService.AddItemToCart(cartId, item);
		}
	}
}
