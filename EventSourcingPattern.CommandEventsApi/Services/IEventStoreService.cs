﻿namespace EventSourcingPattern.CommandEventsApi.Services
{
    public interface IEventStoreService
    {
        Task AppendEventToStreamAsync(string streamName, object eventData, string eventType);
    }
}
