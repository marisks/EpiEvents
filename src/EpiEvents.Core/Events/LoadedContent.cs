using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class LoadedContent : ValueObject<LoadedContent>, INotification
    {
        public LoadedContent(ContentReference contentLink, IContent content)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            if (content == null) throw new ArgumentNullException(nameof(content));
            ContentLink = contentLink;
            Content = content;
        }

        public ContentReference ContentLink { get; }
        public IContent Content { get; }

        public static LoadedContent FromContentEventArgs(ContentEventArgs args)
        {
            return new LoadedContent(args.ContentLink, args.Content);
        }
    }
}