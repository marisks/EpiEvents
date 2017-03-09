using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class DeletingContent : ValueObject<DeletingContent>, INotification
    {
        public DeletingContent(
            ContentReference contentLink,
            AccessLevel requiredAccess,
            IList<ContentReference> deletedDescendents)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            if (deletedDescendents == null) throw new ArgumentNullException(nameof(deletedDescendents));
            ContentLink = contentLink;
            RequiredAccess = requiredAccess;
            DeletedDescendents = deletedDescendents;
        }

        public ContentReference ContentLink { get; }
        public AccessLevel RequiredAccess { get; }
        public IList<ContentReference> DeletedDescendents { get; }

        public static DeletingContent FromDeleteContentArgs(DeleteContentEventArgs arg)
        {
            return new DeletingContent(arg.ContentLink, arg.RequiredAccess, arg.DeletedDescendents);
        }
    }
}