using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;
using EpiEvents.Core.Common;

namespace EpiEvents.Core.Events
{
    public class CopyingContent : ValueObject<CopyingContent>, INotification
    {
        public CopyingContent(
            ContentReference sourceContentLink,
            ContentReference targetLink,
            AccessLevel requiredAccess)
        {
            SourceContentLink = sourceContentLink ?? throw new ArgumentNullException(nameof(sourceContentLink));
            TargetLink = targetLink ?? throw new ArgumentNullException(nameof(targetLink));
            RequiredAccess = requiredAccess;
        }

        public ContentReference SourceContentLink { get; }
        public ContentReference TargetLink { get; }
        public AccessLevel RequiredAccess { get; }

        public static CopyingContent FromContentEventArgs(ContentEventArgs arg)
        {
            var copyArgs = (CopyContentEventArgs) arg;
            return new CopyingContent(copyArgs.SourceContentLink, copyArgs.TargetLink, copyArgs.RequiredAccess);
        }
    }
}