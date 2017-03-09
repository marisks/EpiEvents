using System;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public class CreatedLanguageBranch : ValueObject<CreatedLanguageBranch>, INotification
    {
        public CreatedLanguageBranch(ContentReference contentLink, IContent content, AccessLevel requiredAccess)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            if (content == null) throw new ArgumentNullException(nameof(content));
            ContentLink = contentLink;
            Content = content;
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public IContent Content { get; }
        public AccessLevel RequiredAccess { get; }

        public static CreatedLanguageBranch FromContentEventArgs(ContentEventArgs args)
        {
            return new CreatedLanguageBranch(args.ContentLink, args.Content, args.RequiredAccess);
        }

        public static bool Match(ContentEventArgs args)
        {
            return !ContentReference.IsNullOrEmpty(args.ContentLink);
        }
    }
}