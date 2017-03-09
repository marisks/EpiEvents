using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class LoadingDefaultContent : ValueObject<LoadingDefaultContent>, INotification
    {
        public LoadingDefaultContent(ContentReference parentLink)
        {
            if (parentLink == null) throw new ArgumentNullException(nameof(parentLink));
            ParentLink = parentLink;
        }

        public ContentReference ParentLink { get; }

        public static LoadingDefaultContent FromContentEventArgs(ContentEventArgs args)
        {
            return new LoadingDefaultContent(args.TargetLink);
        }

        public static bool Match(ContentEventArgs args)
        {
            return ContentReference.IsNullOrEmpty(args.ContentLink);
        }
    }
}