using System.Collections.Generic;

namespace JobFinder.Application.Dtos.ServiceResultModels
{
    public static class ListHelpers
    {
        public static void AddMessage(this List<Message<MessageType, string>> obj, MessageType type, string value)
        {
            obj.Add(new Message<MessageType, string>(type, value));
        }
    }
}