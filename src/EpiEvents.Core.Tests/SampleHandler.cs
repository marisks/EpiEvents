using EpiEvents.Core.Events;
using MediatR;

namespace EpiEvents.Core.Tests
{
    public class SampleHandler : NotificationHandler<CreatedContent>
    {
        protected override void HandleCore(CreatedContent notification)
        {
            // Handle your event
        }
    }
}