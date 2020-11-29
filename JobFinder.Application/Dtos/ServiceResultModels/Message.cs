﻿namespace JobFinder.Application.Dtos.ServiceResultModels
{
    public sealed class Message<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Message(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}