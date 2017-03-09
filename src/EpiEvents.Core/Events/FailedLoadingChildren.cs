using System;
using System.Collections.Generic;
using System.Linq;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class FailedLoadingChildren : ValueObject<FailedLoadingChildren>, INotification
    {
        public FailedLoadingChildren(ContentReference contentLink, IEnumerable<IContent> childrenItems)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            ContentLink = contentLink;
            ChildrenItems = childrenItems ?? Enumerable.Empty<IContent>();
        }

        public ContentReference ContentLink { get; }
        public IEnumerable<IContent> ChildrenItems { get; }

        public static FailedLoadingChildren FromChildrenEventArgs(ChildrenEventArgs args)
        {
            return new FailedLoadingChildren(args.ContentLink, args.ChildrenItems);
        }
    }
}