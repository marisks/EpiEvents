using System;
using System.Globalization;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;
using Optional;

namespace EpiEvents.Core.Events
{
    public class DeletedContentLanguage : ValueObject<DeletedContentLanguage>, INotification
    {
        public DeletedContentLanguage(
            ContentReference contentLink,
            IContent content,
            Option<CultureInfo> language,
            Option<CultureInfo> masterLanguage,
            AccessLevel requiredAccess)
        {
            if (contentLink == null) throw new ArgumentNullException(nameof(contentLink));
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (language == null) throw new ArgumentNullException(nameof(language));
            if (masterLanguage == null) throw new ArgumentNullException(nameof(masterLanguage));
            ContentLink = contentLink;
            Content = content;
            Language = language;
            MasterLanguage = masterLanguage;
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public IContent Content { get; }
        public Option<CultureInfo> Language { get; }
        public Option<CultureInfo> MasterLanguage { get; }
        public AccessLevel RequiredAccess { get; }

        public static DeletedContentLanguage FromContentEventArgs(ContentEventArgs arg)
        {
            var contentArgs = (ContentLanguageEventArgs)arg;
            return new DeletedContentLanguage(
                arg.ContentLink,
                arg.Content,
                contentArgs.Language.SomeNotNull(),
                contentArgs.MasterLanguage.SomeNotNull(),
                arg.RequiredAccess);
        }
    }
}
