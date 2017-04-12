using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class FailedLoadingContent : ValueObject<FailedLoadingContent>, INotification
    {
        public FailedLoadingContent(ContentReference contentLink)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
        }

        public ContentReference ContentLink { get; }

        public static FailedLoadingContent FromContentEventArgs(ContentEventArgs args)
        {
            return new FailedLoadingContent(args.ContentLink);
        }
    }
}