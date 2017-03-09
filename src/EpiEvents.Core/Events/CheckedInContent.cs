using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace EpiEvents.Core.Events
{
    public class CheckedInContent : SaveContentNotificationBase<CheckedInContent>
    {
        public CheckedInContent(
            ContentReference contentLink,
            IContent content,
            SaveAction action,
            StatusTransition transition,
            AccessLevel requiredAccess)
            : base(contentLink, content, action, transition, requiredAccess)
        {
        }

        public static CheckedInContent FromContentEventArgs(ContentEventArgs args)
        {
            var saveArgs = (SaveContentEventArgs)args;
            return new CheckedInContent(
                saveArgs.ContentLink,
                saveArgs.Content,
                saveArgs.Action,
                saveArgs.Transition,
                saveArgs.RequiredAccess);
        }
    }
}