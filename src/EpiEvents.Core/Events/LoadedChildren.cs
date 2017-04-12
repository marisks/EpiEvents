using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class LoadedChildren : ValueObject<LoadedChildren>, INotification
    {
        public LoadedChildren(ContentReference contentLink, IEnumerable<IContent> childrenItems)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            ChildrenItems = childrenItems ?? throw new ArgumentNullException(nameof(childrenItems));
        }

        public ContentReference ContentLink { get; }
        public IEnumerable<IContent> ChildrenItems { get; }

        public static LoadedChildren FromChildrenEventArgs(ChildrenEventArgs args)
        {
            return new LoadedChildren(args.ContentLink, args.ChildrenItems);
        }
    }
}