using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class CreatingLanguageBranch : ValueObject<CreatingLanguageBranch>, INotification
    {
        public CreatingLanguageBranch(ContentReference contentLink, AccessLevel requiredAccess)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public AccessLevel RequiredAccess { get; }

        public static CreatingLanguageBranch FromContentEventArgs(ContentEventArgs args)
        {
            return new CreatingLanguageBranch(args.ContentLink, args.RequiredAccess);
        }

        public static bool Match(ContentEventArgs args)
        {
            return !ContentReference.IsNullOrEmpty(args.ContentLink);
        }
    }
}