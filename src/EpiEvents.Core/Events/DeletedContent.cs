using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class DeletedContent : ValueObject<DeletedContent>, INotification
    {
        private const string DeletedItemGuidsKey = "DeletedItemGuids";

        public DeletedContent(
            ContentReference contentLink,
            AccessLevel requiredAccess,
            IList<ContentReference> deletedDescendents,
            IList<Guid> deletedItemGuids)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            if (deletedDescendents == null) throw new ArgumentNullException(nameof(deletedDescendents));
            if (deletedItemGuids == null) throw new ArgumentNullException(nameof(deletedItemGuids));
            ContentLink = contentLink;
            RequiredAccess = requiredAccess;
            DeletedDescendents = deletedDescendents;
            DeletedItemGuids = deletedItemGuids;
        }

        public ContentReference ContentLink { get; }
        public AccessLevel RequiredAccess { get; }
        public IList<ContentReference> DeletedDescendents { get; }
        public IList<Guid> DeletedItemGuids { get; }

        public static DeletedContent FromDeleteContentArgs(DeleteContentEventArgs arg)
        {
            var deletedItemGuids = arg.Items.Contains(DeletedItemGuidsKey)
                ? (IList<Guid>)arg.Items[DeletedItemGuidsKey]
                : new List<Guid>();
            return new DeletedContent(arg.ContentLink, arg.RequiredAccess, arg.DeletedDescendents, deletedItemGuids);
        }
    }
}