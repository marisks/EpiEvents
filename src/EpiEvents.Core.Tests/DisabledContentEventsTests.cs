using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using EpiEvents.Core.Events;
using EPiServer;
using EPiServer.Approvals;
using EPiServer.Core;
using FakeItEasy;
using MediatR;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;

namespace EpiEvents.Core.Tests
{
    public class DisabledContentEventsTests
    {
        private readonly IContentEvents _contentEvents;
        private readonly IMediator _mediator;

        public DisabledContentEventsTests()
        {
            _contentEvents = FakeItEasy.A.Fake<IContentEvents>();
            var approvalEngineEvents = FakeItEasy.A.Fake<IApprovalEngineEvents>();
            _mediator = FakeItEasy.A.Fake<IMediator>();
            var settings = FakeItEasy.A.Fake<ISettings>();
            FakeItEasy.A.CallTo(() => settings.EnableLoadingEvents).Returns(false);

            var eventsMediator = new EventsMediator(_contentEvents, approvalEngineEvents, _mediator, settings);

            eventsMediator.Initialize();
        }

        [FactoryMethodData(typeof(DisabledContentEventsTests), nameof(GetParameters))]
        public void it_does_not_publish_notification_from_event<TEvent, TNotification>(
            TEvent ev, TNotification _, Action<IContentEvents, TEvent> raise)
            where TNotification : ValueObject<TNotification>, INotification
        {
            raise(_contentEvents, ev);

            _mediator.ShouldNotPublishNotificationWith<TNotification>();
        }

        public static IEnumerable<object[]> GetParameters()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoFakeItEasyCustomization());

            foreach (var parameters in FromChildrenEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromContentEventArgs(fixture)) yield return parameters;
        }

        private static IEnumerable<object[]> FromChildrenEventArgs(Fixture fixture)
        {
            var args = fixture.Create<ChildrenEventArgs>();

            yield return new[]
            {
                args,
                LoadingChildren.FromChildrenEventArgs(args),
                A<IContentEvents, ChildrenEventArgs>(
                    (contentEvents, e) => contentEvents.LoadingChildren += Raise.With<ChildrenEventHandler>(null, e))
            };

            yield return new[]
            {
                args,
                LoadedChildren.FromChildrenEventArgs(args),
                A<IContentEvents, ChildrenEventArgs>(
                    (contentEvents, e) => contentEvents.LoadedChildren += Raise.With<ChildrenEventHandler>(null, e))
            };

            yield return new[]
            {
                args,
                FailedLoadingChildren.FromChildrenEventArgs(args),
                A<IContentEvents, ChildrenEventArgs>(
                    (contentEvents, e) => contentEvents.FailedLoadingChildren += Raise.With<ChildrenEventHandler>(null, e))
            };
        }

        private static IEnumerable<object[]> FromContentEventArgs(Fixture fixture)
        {
            var args = fixture.Create<ContentEventArgs>();

            yield return new[]
            {
                args,
                LoadingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.LoadingContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                LoadedContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.LoadedContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                FailedLoadingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.FailedLoadingContent += Raise.With(e))
            };
        }

        private static object A<T1, T2>(Action<T1, T2> action)
        {
            return action;
        }
    }
}