using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class LoadingContent : ValueObject<LoadingContent>, INotification
    {
        public LoadingContent(ContentReference contentLink)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
        }

        public ContentReference ContentLink { get; }

        public static LoadingContent FromContentEventArgs(ContentEventArgs args)
        {
            return new LoadingContent(args.ContentLink);
        }
    }
}