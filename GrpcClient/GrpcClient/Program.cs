using Grpc.Net.Client;
using GrpcClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7284");
var client = new CartItemsService.CartItemsServiceClient(channel);

// client streaming
Console.WriteLine("Client streaming flow:");
var itemsToAdd = Enumerable.Range(51, 4).Select(x => new ItemMessage { Id = x, Name = $"custom name {x}" });
var requestCall = client.AddItemToCartClientStreaming();
foreach(var item in itemsToAdd) {
	await requestCall.RequestStream.WriteAsync(new AddItemToCartRequestMessage { 
		CartId = 10,
		Item = item
	});
	// to test that it will be skipped
	await requestCall.RequestStream.WriteAsync(new AddItemToCartRequestMessage {
		CartId = 4,
		Item = itemsToAdd.First()
	});
}

await requestCall.RequestStream.CompleteAsync();

CartItemsReplyMessage clientStreamingResponse = await requestCall.ResponseAsync;

// server streaming
Console.ReadKey();
Console.WriteLine("Server streaming flow:");
var serverData = client.GetCartItemsServerStreaming(new CartItemsRequestMessage { CartId = 4 });
var responseStream = serverData.ResponseStream;

while (await responseStream.MoveNext(new CancellationToken())) {

	ItemMessage response = responseStream.Current;
	Console.WriteLine(response.Id);
}

Console.WriteLine("Unary flow:");

// unary call
var reply = client.GetCartItemsUnary(new CartItemsRequestMessage { CartId = 4 });
Console.WriteLine("finish");
Console.ReadKey();
