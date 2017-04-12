using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;
using Optional;

namespace EpiEvents.Core.Events
{
    public class LoadedContent : ValueObject<LoadedContent>, INotification
    {
        public LoadedContent(ContentReference contentLink, Option<IContent> content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            Content = content;
        }

        public ContentReference ContentLink { get; }
        public Option<IContent> Content { get; }

        public static LoadedContent FromContentEventArgs(ContentEventArgs args)
        {
            return new LoadedContent(args.ContentLink, args.Content.SomeNotNull());
        }
    }
}