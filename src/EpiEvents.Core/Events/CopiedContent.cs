using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;

namespace EpiEvents.Core.Events
{
    public class CopiedContent
    {
        public CopiedContent(
            ContentReference sourceContentLink,
            ContentReference targetLink,
            AccessLevel requiredAccess)
        {
            if (sourceContentLink == null) throw new ArgumentNullException(nameof(sourceContentLink));
            if (targetLink == null) throw new ArgumentNullException(nameof(targetLink));
            SourceContentLink = sourceContentLink;
            TargetLink = targetLink;
            RequiredAccess = requiredAccess;
        }

        public ContentReference SourceContentLink { get; }
        public ContentReference TargetLink { get; }
        public AccessLevel RequiredAccess { get; }

        public static CopiedContent FromContentEventArgs(ContentEventArgs arg)
        {
            var copyArgs = (CopyContentEventArgs)arg;
            return new CopiedContent(copyArgs.SourceContentLink, copyArgs.TargetLink, copyArgs.RequiredAccess);
        }
    }
}
