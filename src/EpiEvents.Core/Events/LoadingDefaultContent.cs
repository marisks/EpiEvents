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
            ParentLink = parentLink ?? throw new ArgumentNullException(nameof(parentLink));
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