using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using EpiEvents.Core.Events;
using EPiServer;
using EPiServer.Core;
using FakeItEasy;
using MediatR;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using EPiServer.Approvals;

namespace EpiEvents.Core.Tests
{
    public class AllEnabledContentEventsTests
    {
        private readonly IContentEvents _contentEvents;
        private readonly IMediator _mediator;
        private readonly Fixture _fixture;

        public AllEnabledContentEventsTests()
        {
            _contentEvents = FakeItEasy.A.Fake<IContentEvents>();
            var approvalEngineEvents = FakeItEasy.A.Fake<IApprovalEngineEvents>();
            _mediator = FakeItEasy.A.Fake<IMediator>();
            var settings = FakeItEasy.A.Fake<ISettings>();
            FakeItEasy.A.CallTo(() => settings.EnableLoadingEvents).Returns(true);

            _fixture = new Fixture();
            _fixture.Customize(new AutoFakeItEasyCustomization());

            var eventsMediator = new EventsMediator(_contentEvents, approvalEngineEvents, _mediator, settings);

            eventsMediator.Initialize();
        }

        [FactoryMethodData(typeof(AllEnabledContentEventsTests), nameof(GetParameters))]
        public void it_publishes_notification_from_event<TEvent, TNotification>(
            TEvent ev, TNotification expected, Action<IContentEvents, TEvent> raise)
            where TNotification : ValueObject<TNotification>, INotification
        {
            raise(_contentEvents, ev);

            _mediator.ShouldPublishNotificationWith<TNotification>(actual => actual == expected);
        }

        public static IEnumerable<object[]> GetParameters()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoFakeItEasyCustomization());

            foreach (var parameters in FromChildrenEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromContentEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromSaveContentEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromDeleteContentEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromContentLanguageEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromMoveContentEventArgs(fixture)) yield return parameters;
        }

        private static IEnumerable<object[]> FromMoveContentEventArgs(Fixture fixture)
        {
            var args = fixture.Create<MoveContentEventArgs>();

            yield return new[]
            {
                args,
                MovingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.MovingContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                MovedContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.MovedContent += Raise.With(e))
            };
        }

        private static IEnumerable<object[]> FromContentLanguageEventArgs(Fixture fixture)
        {
            var args = fixture.Create<ContentLanguageEventArgs>();

            yield return new[]
            {
                args,
                DeletingContentLanguage.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.DeletingContentLanguage += Raise.With(e))
            };

            yield return new[]
            {
                args,
                DeletedContentLanguage.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.DeletedContentLanguage += Raise.With(e))
            };
        }

        private static IEnumerable<object[]> FromDeleteContentEventArgs(Fixture fixture)
        {
            var args = fixture.Create<DeleteContentEventArgs>();

            yield return new[]
            {
                args,
                DeletingContent.FromDeleteContentArgs(args),
                A<IContentEvents, DeleteContentEventArgs>(
                    (contentEvents, e) => contentEvents.DeletingContent += Raise.With(e))
            };

            var deletedItemGuids = fixture.CreateMany<Guid>().ToList();
            args.Items["DeletedItemGuids"] = deletedItemGuids;
            yield return new[]
            {
                args,
                DeletedContent.FromDeleteContentArgs(args),
                A<IContentEvents, DeleteContentEventArgs>(
                    (contentEvents, e) => contentEvents.DeletedContent += Raise.With(e))
            };
        }

        private static IEnumerable<object[]> FromSaveContentEventArgs(Fixture fixture)
        {
            var args = fixture.Create<SaveContentEventArgs>();

            yield return new[]
            {
                args,
                PublishingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.PublishingContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                PublishedContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.PublishedContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                CheckingInContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.CheckingInContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                CheckedInContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.CheckedInContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                RequestingApproval.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.RequestingApproval += Raise.With(e))
            };

            yield return new[]
            {
                args,
                RequestedApproval.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.RequestedApproval += Raise.With(e))
            };

            yield return new[]
            {
                args,
                RejectingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.RejectingContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                RejectedContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.RejectedContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                CheckingOutContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.CheckingOutContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                CheckedOutContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.CheckedOutContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                SchedulingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.SchedulingContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                ScheduledContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.ScheduledContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                CreatingContentLanguage.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.CreatingContentLanguage += Raise.With(e))
            };

            yield return new[]
            {
                args,
                CreatedContentLanguage.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.CreatedContentLanguage += Raise.With(e))
            };

            yield return new[]
            {
                args,
                SavingContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.SavingContent += Raise.With(e))
            };

            yield return new[]
            {
                args,
                SavedContent.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.SavedContent += Raise.With(e))
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

            yield return new[]
            {
                args,
                DeletingContentVersion.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.DeletingContentVersion += Raise.With(e))
            };

            yield return new[]
            {
                args,
                DeletedContentVersion.FromContentEventArgs(args),
                A<IContentEvents, ContentEventArgs>(
                    (contentEvents, e) => contentEvents.DeletedContentVersion += Raise.With(e))
            };
        }

        private static IEnumerable<object[]> FromChildrenEventArgs(Fixture fixture)
        {
            var args = fixture.Create<ChildrenEventArgs>();

            yield return new[]
            {
                args,
                LoadingChildren.FromChildrenEventArgs(args),
                A<IContentEvents, ChildrenEventArgs>(
                    (contentEvents, e) => contentEvents.LoadingChildren += Raise.FreeForm<ChildrenEventHandler>.With(null, e))
            };

            yield return new[]
            {
                args,
                LoadedChildren.FromChildrenEventArgs(args),
                A<IContentEvents, ChildrenEventArgs>(
                    (contentEvents, e) => contentEvents.LoadedChildren += Raise.FreeForm<ChildrenEventHandler>.With(null, e))
            };

            yield return new[]
            {
                args,
                FailedLoadingChildren.FromChildrenEventArgs(args),
                A<IContentEvents, ChildrenEventArgs>(
                    (contentEvents, e) => contentEvents.FailedLoadingChildren += Raise.FreeForm<ChildrenEventHandler>.With(null, e))
            };
        }

        public void it_publishes_LoadingDefaultContent_when_contentLink_is_empty()
        {
            var ev = ContentEventArgWithEmptyContentLink();
            var expected = LoadingDefaultContent.FromContentEventArgs(ev);

            _contentEvents.LoadingDefaultContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<LoadingDefaultContent>(actual => actual == expected);
        }

        public void it_does_not_publish_LoadingDefaultContent_when_contentLink_is_not_empty()
        {
            var ev = _fixture.Create<ContentEventArgs>();

            _contentEvents.LoadingDefaultContent += Raise.With(ev);

            _mediator.ShouldNotPublishNotificationWith<LoadingDefaultContent>();
        }

        public void it_publishes_LoadedDefaultContent_when_contentLink_is_empty()
        {
            var ev = ContentEventArgWithEmptyContentLink();
            var expected = LoadedDefaultContent.FromContentEventArgs(ev);

            _contentEvents.LoadedDefaultContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<LoadedDefaultContent>(actual => actual == expected);
        }

        public void it_does_not_publish_LoadedDefaultContent_when_contentLink_is_not_empty()
        {
            var ev = _fixture.Create<ContentEventArgs>();

            _contentEvents.LoadedDefaultContent += Raise.With(ev);

            _mediator.ShouldNotPublishNotificationWith<LoadedDefaultContent>();
        }

        public void it_publishes_CreatingLanguageBranch_when_contentLink_is_not_empty()
        {
            var ev = _fixture.Create<ContentEventArgs>();
            var expected = CreatingLanguageBranch.FromContentEventArgs(ev);

            _contentEvents.LoadingDefaultContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<CreatingLanguageBranch>(actual => actual == expected);
        }

        public void it_does_not_publish_CreatingLanguageBranch_when_contentLink_is_empty()
        {
            var ev = ContentEventArgWithEmptyContentLink();

            _contentEvents.LoadingDefaultContent += Raise.With(ev);

            _mediator.ShouldNotPublishNotificationWith<CreatingLanguageBranch>();
        }

        public void it_publishes_CreatedLanguageBranch_when_contentLink_is_not_empty()
        {
            var ev = _fixture.Create<ContentEventArgs>();
            var expected = CreatedLanguageBranch.FromContentEventArgs(ev);

            _contentEvents.LoadedDefaultContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<CreatedLanguageBranch>(actual => actual == expected);
        }

        public void it_does_not_publish_CreatedLanguageBranch_when_contentLink_is_empty()
        {
            var ev = ContentEventArgWithEmptyContentLink();

            _contentEvents.LoadedDefaultContent += Raise.With(ev);

            _mediator.ShouldNotPublishNotificationWith<CreatedLanguageBranch>();
        }

        public void it_publishes_CreatingContent_when_SaveContentEventArgs()
        {
            var ev = (ContentEventArgs)_fixture.Create<SaveContentEventArgs>();
            var expected = CreatingContent.FromContentEventArgs(ev);

            _contentEvents.CreatingContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<CreatingContent>(actual => actual == expected);
        }

        public void it_publishes_CreatedContent_when_SaveContentEventArgs()
        {
            var ev = (ContentEventArgs)_fixture.Create<SaveContentEventArgs>();
            var expected = CreatedContent.FromContentEventArgs(ev);

            _contentEvents.CreatedContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<CreatedContent>(actual => actual == expected);
        }

        public void it_publishes_CopyingContent_when_CopyContentEventArgs()
        {
            var ev = (ContentEventArgs)_fixture.Create<CopyContentEventArgs>();
            var expected = CopyingContent.FromContentEventArgs(ev);

            _contentEvents.CreatingContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<CopyingContent>(actual => actual == expected);
        }

        public void it_publishes_CopyiedContent_when_CopyContentEventArgs()
        {
            var ev = (ContentEventArgs)_fixture.Create<CopyContentEventArgs>();
            var expected = CopyiedContent.FromContentEventArgs(ev);

            _contentEvents.CreatedContent += Raise.With(ev);

            _mediator.ShouldPublishNotificationWith<CopyiedContent>(actual => actual == expected);
        }

        private ContentEventArgs ContentEventArgWithEmptyContentLink()
        {
            return _fixture.Build<ContentEventArgs>()
                .With(x => x.ContentLink, ContentReference.EmptyReference)
                .Create();
        }

        private static object A<T1, T2>(Action<T1, T2> action)
        {
            return action;
        }
    }
}