using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace EpiEvents.Core.Events
{
    public class ScheduledContent : SaveContentNotificationBase<ScheduledContent>
    {
        public ScheduledContent(
            ContentReference contentLink,
            IContent content,
            SaveAction action,
            StatusTransition transition,
            AccessLevel requiredAccess)
            : base(contentLink, content, action, transition, requiredAccess)
        {
        }

        public static ScheduledContent FromContentEventArgs(ContentEventArgs args)
        {
            var saveArgs = (SaveContentEventArgs)args;
            return new ScheduledContent(
                saveArgs.ContentLink,
                saveArgs.Content,
                saveArgs.Action,
                saveArgs.Transition,
                saveArgs.RequiredAccess);
        }
    }
}
