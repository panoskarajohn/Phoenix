﻿using Community.Rabbit.Abstractions.Conventions;

namespace Community.Rabbit.Abstractions.Client;

public interface IRabbitMqClient
{
    void Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
        string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null);
}