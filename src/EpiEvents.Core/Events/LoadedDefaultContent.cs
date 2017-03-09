using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class LoadedDefaultContent : ValueObject<LoadedDefaultContent>, INotification
    {
        public LoadedDefaultContent(ContentReference parentLink, IContent content)
        {
            if (parentLink == null) throw new ArgumentNullException(nameof(parentLink));
            if (content == null) throw new ArgumentNullException(nameof(content));
            ParentLink = parentLink;
            Content = content;
        }

        public ContentReference ParentLink { get; }
        public IContent Content { get; }

        public static LoadedDefaultContent FromContentEventArgs(ContentEventArgs args)
        {
            return new LoadedDefaultContent(args.TargetLink, args.Content);
        }

        public static bool Match(ContentEventArgs args)
        {
            return ContentReference.IsNullOrEmpty(args.ContentLink);
        }
    }
}