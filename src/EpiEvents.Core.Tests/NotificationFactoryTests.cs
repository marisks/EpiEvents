using System;
using System.Collections.Generic;
using System.Linq;
using EpiEvents.Core.Events;
using EPiServer;
using FluentAssertions;
using Optional;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;

namespace EpiEvents.Core.Tests
{
    public class NotificationFactoryTests
    {
        [FactoryMethodData(typeof(NotificationFactoryTests), nameof(GetParameters))]
        public void it_creates_notification_from_event<TEvent, T>(TEvent ev, T expected, Func<TEvent, T> act)
        {
            var actual = act(ev);

            actual.ShouldBeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetParameters()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoFakeItEasyCustomization());

            foreach (var parameters in FromSaveContentArgs(fixture)) yield return parameters;
            foreach (var parameters in FromContentArgs(fixture)) yield return parameters;
            foreach (var parameters in FromChildrenArgs(fixture)) yield return parameters;
            foreach (var parameters in FromDeleteContentArgs(fixture)) yield return parameters;
            foreach (var parameters in FromContentLanguageArgs(fixture)) yield return parameters;
            foreach (var parameters in FromMoveContentArgs(fixture)) yield return parameters;
            foreach (var parameters in FromCopyContentArgs(fixture)) yield return parameters;
        }

        private static IEnumerable<object[]> FromCopyContentArgs(Fixture fixture)
        {
            var args = fixture.Create<CopyContentEventArgs>();

            yield return new object[]
            {
                args,
                new CopyingContent(args.SourceContentLink, args.TargetLink, args.RequiredAccess),
                (Func<CopyContentEventArgs, CopyingContent>) CopyingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CopiedContent(args.SourceContentLink, args.TargetLink, args.RequiredAccess),
                (Func<CopyContentEventArgs, CopiedContent>) CopiedContent.FromContentEventArgs
            };
        }

        private static IEnumerable<object[]> FromMoveContentArgs(Fixture fixture)
        {
            var args = fixture.Create<MoveContentEventArgs>();

            yield return new object[]
            {
                args,
                new MovingContent(args.ContentLink, args.TargetLink, args.OriginalParent, args.Descendents, args.Content),
                (Func<MoveContentEventArgs, MovingContent>) MovingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new MovedContent(args.ContentLink, args.TargetLink, args.OriginalParent, args.Descendents, args.Content),
                (Func<MoveContentEventArgs, MovedContent>) MovedContent.FromContentEventArgs
            };
        }

        private static IEnumerable<object[]> FromContentLanguageArgs(Fixture fixture)
        {
            var args = fixture.Create<ContentLanguageEventArgs>();

            yield return new object[]
            {
                args,
                new DeletingContentLanguage(args.ContentLink, args.Content, args.Language.SomeNotNull(), args.MasterLanguage.SomeNotNull(), args.RequiredAccess),
                (Func<ContentLanguageEventArgs, DeletingContentLanguage>) DeletingContentLanguage.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new DeletedContentLanguage(args.ContentLink, args.Content, args.Language.SomeNotNull(), args.MasterLanguage.SomeNotNull(), args.RequiredAccess),
                (Func<ContentLanguageEventArgs, DeletedContentLanguage>) DeletedContentLanguage.FromContentEventArgs
            };
        }

        private static IEnumerable<object[]> FromDeleteContentArgs(Fixture fixture)
        {
            var args = fixture.Create<DeleteContentEventArgs>();

            yield return new object[]
            {
                args,
                new DeletingContent(args.ContentLink, args.RequiredAccess, args.DeletedDescendents),
                (Func<DeleteContentEventArgs, DeletingContent>) DeletingContent.FromDeleteContentArgs
            };

            var deletedItemGuids = fixture.CreateMany<Guid>().ToList();
            args.Items["DeletedItemGuids"] = deletedItemGuids;
            yield return new object[]
            {
                args,
                new DeletedContent(args.ContentLink, args.RequiredAccess, args.DeletedDescendents, (IList<Guid>) args.Items["DeletedItemGuids"]),
                (Func<DeleteContentEventArgs, DeletedContent>) DeletedContent.FromDeleteContentArgs
            };
        }

        private static IEnumerable<object[]> FromChildrenArgs(Fixture fixture)
        {
            var args = fixture.Create<ChildrenEventArgs>();

            yield return new object[]
            {
                args,
                new LoadingChildren(
                    args.ContentLink),
                (Func<ChildrenEventArgs, LoadingChildren>) LoadingChildren.FromChildrenEventArgs
            };

            yield return new object[]
            {
                args,
                new LoadedChildren(
                    args.ContentLink,
                    args.ChildrenItems),
                (Func<ChildrenEventArgs, LoadedChildren>) LoadedChildren.FromChildrenEventArgs
            };

            yield return new object[]
            {
                args,
                new FailedLoadingChildren(
                    args.ContentLink,
                    args.ChildrenItems),
                (Func<ChildrenEventArgs, FailedLoadingChildren>) FailedLoadingChildren.FromChildrenEventArgs
            };
        }

        private static IEnumerable<object[]> FromContentArgs(Fixture fixture)
        {
            var args = fixture.Create<ContentEventArgs>();

            yield return new object[]
            {
                args,
                new CreatedLanguageBranch(
                    args.ContentLink,
                    args.Content,
                    args.RequiredAccess),
                (Func<ContentEventArgs, CreatedLanguageBranch>) CreatedLanguageBranch.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CreatingLanguageBranch(
                    args.ContentLink,
                    args.RequiredAccess),
                (Func<ContentEventArgs, CreatingLanguageBranch>) CreatingLanguageBranch.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new LoadedContent(
                    args.ContentLink,
                    args.Content),
                (Func<ContentEventArgs, LoadedContent>) LoadedContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new LoadingContent(
                    args.ContentLink),
                (Func<ContentEventArgs, LoadingContent>) LoadingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new FailedLoadingContent(
                    args.ContentLink),
                (Func<ContentEventArgs, FailedLoadingContent>) FailedLoadingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new LoadedDefaultContent(
                    args.TargetLink,
                    args.Content),
                (Func<ContentEventArgs, LoadedDefaultContent>) LoadedDefaultContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new LoadingDefaultContent(
                    args.TargetLink),
                (Func<ContentEventArgs, LoadingDefaultContent>) LoadingDefaultContent.FromContentEventArgs
            };

            yield return new object[]
           {
                args,
                new DeletingContentVersion(
                    args.ContentLink,
                    args.RequiredAccess),
                (Func<ContentEventArgs, DeletingContentVersion>) DeletingContentVersion.FromContentEventArgs
           };

            yield return new object[]
           {
                args,
                new DeletedContentVersion(
                    args.ContentLink,
                    args.RequiredAccess),
                (Func<ContentEventArgs, DeletedContentVersion>) DeletedContentVersion.FromContentEventArgs
           };
        }

        private static IEnumerable<object[]> FromSaveContentArgs(Fixture fixture)
        {
            var args = fixture.Create<SaveContentEventArgs>();

            yield return new object[]
            {
                args,
                new CheckedInContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CheckedInContent>) CheckedInContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CheckingInContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CheckingInContent>) CheckingInContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new PublishedContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, PublishedContent>) PublishedContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new PublishingContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, PublishingContent>) PublishingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new RequestingApproval(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, RequestingApproval>) RequestingApproval.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new RequestedApproval(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, RequestedApproval>) RequestedApproval.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new RejectingContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, RejectingContent>) RejectingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new RejectedContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, RejectedContent>) RejectedContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CheckingOutContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CheckingOutContent>) CheckingOutContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CheckedOutContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CheckedOutContent>) CheckedOutContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new SchedulingContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, SchedulingContent>) SchedulingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new ScheduledContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, ScheduledContent>) ScheduledContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CreatingContentLanguage(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CreatingContentLanguage>) CreatingContentLanguage.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CreatedContentLanguage(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CreatedContentLanguage>) CreatedContentLanguage.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CreatingContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CreatingContent>) CreatingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new CreatedContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, CreatedContent>) CreatedContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new SavingContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, SavingContent>) SavingContent.FromContentEventArgs
            };

            yield return new object[]
            {
                args,
                new SavedContent(
                    args.ContentLink,
                    args.Content,
                    args.Action,
                    args.Transition,
                    args.RequiredAccess),
                (Func<SaveContentEventArgs, SavedContent>) SavedContent.FromContentEventArgs
            };
        }
    }
}