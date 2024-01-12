using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7284");
var client = new CartItemsService.CartItemsServiceClient(channel);

#region bi-directional streaming
Console.WriteLine("Bi-directional streaming flow:");
var itemsToAdd = Enumerable.Range(20, 4).Select(x => new ItemMessage { Id = x, Name = $"custom name {x}", CartId = 5 });
var call = client.AddItemToCartBiDirectionalStreaming();

var readTask = Task.Run(async () => {
	await foreach (var item in call.ResponseStream.ReadAllAsync()) {
		Console.WriteLine($"Item {item.Id} was added to cart {item.CartId}");
	}
});

foreach (var item in itemsToAdd) {
	await call.RequestStream.WriteAsync(item);
	await Task.Delay(2000);
}

await call.RequestStream.CompleteAsync(); // important
await readTask; // await after all messages were sent
#endregion bi-directional streaming

#region client streaming
//Console.WriteLine("Client streaming flow:");
//var itemsToAdd = Enumerable.Range(70, 3).Select(x => new ItemMessage { Id = x, Name = $"custom name {x}", CartId = 3 });
//var requestCall = client.AddItemToCartClientStreaming();

//foreach (var item in itemsToAdd) {
//	await requestCall.RequestStream.WriteAsync(item);
//}

//await requestCall.RequestStream.CompleteAsync();

//AddItemsReplyMessage clientStreamingResponse = await requestCall.ResponseAsync;
//Console.WriteLine($"Added {clientStreamingResponse.AddedItemsCount} items");
#endregion client streaming

#region server streaming
Console.ReadKey();
Console.WriteLine("Server streaming flow:");
var serverData = client.GetCartItemsServerStreaming(new CartItemsRequestMessage { CartId = 5 }); //3
var responseStream = serverData.ResponseStream;

while (await responseStream.MoveNext(new CancellationToken())) {

	ItemMessage response = responseStream.Current;
	Console.WriteLine(response.Id);
}
#endregion server streaming

Console.WriteLine("Unary flow:");

#region unary call
//var reply = client.GetCartItemsUnary(new CartItemsRequestMessage { CartId = 3 });
//Console.WriteLine("finish");
//Console.ReadKey();
#endregion unary call
