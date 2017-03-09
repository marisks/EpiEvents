using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace EpiEvents.Core.Events
{
    public class SavingContent : SaveContentNotificationBase<SavingContent>
    {
        public SavingContent(
            ContentReference contentLink,
            IContent content,
            SaveAction action,
            StatusTransition transition,
            AccessLevel requiredAccess)
            : base(contentLink, content, action, transition, requiredAccess)
        {
        }

        public static SavingContent FromContentEventArgs(ContentEventArgs args)
        {
            var saveArgs = (SaveContentEventArgs)args;
            return new SavingContent(
                saveArgs.ContentLink,
                saveArgs.Content,
                saveArgs.Action,
                saveArgs.Transition,
                saveArgs.RequiredAccess);
        }
    }
}
