using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class LoadingChildren : ValueObject<LoadingChildren>, INotification
    {
        public LoadingChildren(ContentReference contentLink)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
        }

        public ContentReference ContentLink { get; }

        public static LoadingChildren FromChildrenEventArgs(ChildrenEventArgs args)
        {
            return new LoadingChildren(args.ContentLink);
        }
    }
}