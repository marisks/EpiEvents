using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class DeletingContentVersion : ValueObject<DeletingContentVersion>, INotification
    {
        public DeletingContentVersion(ContentReference contentLink, AccessLevel requiredAccess)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            ContentLink = contentLink;
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public AccessLevel RequiredAccess { get; }

        public static DeletingContentVersion FromContentEventArgs(ContentEventArgs args)
        {
            return new DeletingContentVersion(args.ContentLink, args.RequiredAccess);
        }
    }
}
