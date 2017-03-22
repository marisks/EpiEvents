using EpiEvents.Core.Events;
using MediatR;

namespace EpiEvents.Core.Tests
{
    public class SampleHandler : INotificationHandler<CreatedContent>
    {
        public void Handle(CreatedContent notification)
        {
            // Handle your event
        }
    }
}