using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class DeletedContentVersion : ValueObject<DeletedContentVersion>, INotification
    {
        public DeletedContentVersion(ContentReference contentLink, AccessLevel requiredAccess)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public AccessLevel RequiredAccess { get; }

        public static DeletedContentVersion FromContentEventArgs(ContentEventArgs args)
        {
            return new DeletedContentVersion(args.ContentLink, args.RequiredAccess);
        }
    }
}
