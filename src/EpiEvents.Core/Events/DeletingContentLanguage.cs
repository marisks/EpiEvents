﻿using System;
using System.Globalization;
using EpiEvents.Core.Common;
using EPiServer;
using EPiServer.Core;
using EPiServer.Security;
using MediatR;
using Optional;

namespace EpiEvents.Core.Events
{
    public class DeletingContentLanguage : ValueObject<DeletingContentLanguage>, INotification
    {
        public DeletingContentLanguage(
            ContentReference contentLink,
            IContent content,
            Option<CultureInfo> language,
            Option<CultureInfo> masterLanguage,
            AccessLevel requiredAccess)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Language = language;
            MasterLanguage = masterLanguage;
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public IContent Content { get; }
        public Option<CultureInfo> Language { get; }
        public Option<CultureInfo> MasterLanguage { get; }
        public AccessLevel RequiredAccess { get; }

        public static DeletingContentLanguage FromContentEventArgs(ContentEventArgs arg)
        {
            var contentArgs = (ContentLanguageEventArgs)arg;
            return new DeletingContentLanguage(
                arg.ContentLink,
                arg.Content,
                contentArgs.Language.SomeNotNull(),
                contentArgs.MasterLanguage.SomeNotNull(),
                arg.RequiredAccess);
        }
    }
}