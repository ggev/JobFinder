using System.Collections.Generic;

namespace JobFinder.Application.Dtos.ServiceResultModels
{
    public sealed class ServiceResult
    {
        public ServiceResult()
        {
            Messages = new List<Message<MessageType, string>>();
        }

        public bool Success { get; set; }
        public object Data { get; set; }
        public List<Message<MessageType, string>> Messages { get; set; }
    }
}
