using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class CopyiedContent : ValueObject<CopyiedContent>, INotification
    {
        public CopyiedContent(
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

        public static CopyiedContent FromContentEventArgs(ContentEventArgs arg)
        {
            var copyArgs = (CopyContentEventArgs)arg;
            return new CopyiedContent(copyArgs.SourceContentLink, copyArgs.TargetLink, copyArgs.RequiredAccess);
        }
    }
}