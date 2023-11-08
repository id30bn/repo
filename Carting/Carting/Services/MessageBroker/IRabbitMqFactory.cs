﻿using MessageBroker.Shared.Interfaces;

namespace Carting.Services.MessageBroker
{
	public interface IRabbitMqFactory
	{
		public IRabbitMqReceiverService CreateReceiver(string routingKey);
	}
}
