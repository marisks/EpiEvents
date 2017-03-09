using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using MediatR;
using EpiEvents.Core.Common;

namespace EpiEvents.Core.Events
{
    public class MovingContent : ValueObject<MovingContent>, INotification
    {
        public MovingContent(
            ContentReference contentLink,
            ContentReference targetLink,
            ContentReference originalParent,
            IEnumerable<ContentReference> descendents,
            IContent content)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            if (targetLink == null) throw new ArgumentNullException(nameof(targetLink));
            if (originalParent == null) throw new ArgumentNullException(nameof(originalParent));
            if (descendents == null) throw new ArgumentNullException(nameof(descendents));
            if (content == null) throw new ArgumentNullException(nameof(content));
            ContentLink = contentLink;
            TargetLink = targetLink;
            OriginalParent = originalParent;
            Descendents = descendents;
            Content = content;
        }

        public ContentReference ContentLink { get; }
        public ContentReference TargetLink { get; }
        public ContentReference OriginalParent { get; }
        public IEnumerable<ContentReference> Descendents { get; }
        public IContent Content { get; }

        public static MovingContent FromContentEventArgs(ContentEventArgs arg)
        {
            var moveArgs = (MoveContentEventArgs) arg;
            return new MovingContent(
                arg.ContentLink,
                arg.TargetLink,
                moveArgs.OriginalParent,
                moveArgs.Descendents,
                arg.Content);
        }
    }
}