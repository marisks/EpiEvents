using System;
using System.Collections.Generic;
using EPiServer;
using EPiServer.Core;
using MediatR;
using EpiEvents.Core.Common;

namespace EpiEvents.Core.Events
{
    public class MovedContent : ValueObject<MovedContent>, INotification
    {
        public MovedContent(
            ContentReference contentLink,
            ContentReference targetLink,
            ContentReference originalParent,
            IEnumerable<ContentReference> descendents,
            IContent content)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            TargetLink = targetLink ?? throw new ArgumentNullException(nameof(targetLink));
            OriginalParent = originalParent ?? throw new ArgumentNullException(nameof(originalParent));
            Descendents = descendents ?? throw new ArgumentNullException(nameof(descendents));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public ContentReference ContentLink { get; }
        public ContentReference TargetLink { get; }
        public ContentReference OriginalParent { get; }
        public IEnumerable<ContentReference> Descendents { get; }
        public IContent Content { get; }

        public static MovedContent FromContentEventArgs(ContentEventArgs arg)
        {
            var moveArgs = (MoveContentEventArgs)arg;
            return new MovedContent(
                arg.ContentLink,
                arg.TargetLink,
                moveArgs.OriginalParent,
                moveArgs.Descendents,
                arg.Content);
        }
    }
}
