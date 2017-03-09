using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;

namespace EpiEvents.Core.Events
{
    public class RequestedApproval : SaveContentNotificationBase<RequestedApproval>
    {
        public RequestedApproval(
            ContentReference contentLink,
            IContent content,
            SaveAction action,
            StatusTransition transition,
            AccessLevel requiredAccess)
            : base(contentLink, content, action, transition, requiredAccess)
        {
        }

        public static RequestedApproval FromContentEventArgs(ContentEventArgs args)
        {
            var saveArgs = (SaveContentEventArgs)args;
            return new RequestedApproval(
                saveArgs.ContentLink,
                saveArgs.Content,
                saveArgs.Action,
                saveArgs.Transition,
                saveArgs.RequiredAccess);
        }
    }
}