using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace EpiEvents.Core.Events
{
    public class RejectingContent : SaveContentNotificationBase<RejectingContent>
    {
        public RejectingContent(
            ContentReference contentLink,
            IContent content,
            SaveAction action,
            StatusTransition transition,
            AccessLevel requiredAccess)
            : base(contentLink, content, action, transition, requiredAccess)
        {
        }

        public static RejectingContent FromContentEventArgs(ContentEventArgs args)
        {
            var saveArgs = (SaveContentEventArgs)args;
            return new RejectingContent(
                saveArgs.ContentLink,
                saveArgs.Content,
                saveArgs.Action,
                saveArgs.Transition,
                saveArgs.RequiredAccess);
        }
    }
}
