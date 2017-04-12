using System;
using EpiEvents.Core.Common;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using MediatR;

namespace EpiEvents.Core.Events
{
    public abstract class SaveContentNotificationBase<T> : ValueObject<T>, INotification
        where T : ValueObject<T>
    {
        protected SaveContentNotificationBase(
            ContentReference contentLink,
            IContent content,
            SaveAction action,
            StatusTransition transition,
            AccessLevel requiredAccess)
        {
            ContentLink = contentLink ?? throw new ArgumentNullException(nameof(contentLink));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Action = action;
            Transition = transition;
            RequiredAccess = requiredAccess;
        }

        public ContentReference ContentLink { get; }
        public IContent Content { get; }
        public SaveAction Action { get; }
        public StatusTransition Transition { get; }
        public AccessLevel RequiredAccess { get; }
    }
}