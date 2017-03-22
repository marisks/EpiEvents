using System;
using EpiEvents.Core.Events;
using EPiServer;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core
{
    public class EventsMediator
    {
        private readonly IContentEvents _contentEvents;
        private readonly IMediator _mediator;

        public EventsMediator(IContentEvents contentEvents, IMediator mediator)
        {
            if (contentEvents == null) throw new ArgumentNullException(nameof(contentEvents));
            if (mediator == null) throw new ArgumentNullException(nameof(mediator));
            _contentEvents = contentEvents;
            _mediator = mediator;
        }

        public void Initialize()
        {
            _contentEvents.LoadingChildren += OnLoadingChildren;
            _contentEvents.LoadedChildren += OnLoadedChildren;
            _contentEvents.FailedLoadingChildren += OnFailedLoadingChildren;
            _contentEvents.LoadingContent += OnLoadingContent;
            _contentEvents.LoadedContent += OnLoadedContent;
            _contentEvents.FailedLoadingContent += OnFailedLoadingContent;
            _contentEvents.LoadingDefaultContent += OnLoadingDefaultContent;
            _contentEvents.LoadedDefaultContent += OnLoadedDefaultContent;
            _contentEvents.PublishingContent += OnPublishingContent;
            _contentEvents.PublishedContent += OnPublishedContent;
            _contentEvents.CheckedInContent += OnCheckedInContent;
            _contentEvents.CheckingInContent += OnCheckingInContent;
            _contentEvents.RequestingApproval += OnRequestingApproval;
            _contentEvents.RequestedApproval += OnRequestedApproval;
            _contentEvents.RejectingContent += OnRejectingContent;
            _contentEvents.RejectedContent += OnRejectedContent;
            _contentEvents.CheckingOutContent += OnCheckingOutContent;
            _contentEvents.CheckedOutContent += OnCheckedOutContent;
            _contentEvents.SchedulingContent += OnSchedulingContent;
            _contentEvents.ScheduledContent += OnScheduledContent;
            _contentEvents.DeletingContent += OnDeletingContent;
            _contentEvents.DeletedContent += OnDeletedContent;
            _contentEvents.CreatingContentLanguage += OnCreatingContentLanguage;
            _contentEvents.CreatedContentLanguage += OnCreatedContentLanguage;
            _contentEvents.DeletingContentLanguage += OnDeletingContentLanguage;
            _contentEvents.DeletedContentLanguage += OnDeletedContentLanguage;
            _contentEvents.MovingContent += OnMovingContent;
            _contentEvents.MovedContent += OnMovedContent;
            _contentEvents.SavingContent += OnSavingContent;
            _contentEvents.SavedContent += OnSavedContent;
            _contentEvents.DeletingContentVersion += OnDeletingContentVersion;
            _contentEvents.DeletedContentVersion += OnDeletedContentVersion;
            _contentEvents.CreatingContent += OnCreatingContent;
        }

        private void OnCreatingContent(object sender, ContentEventArgs e)
        {
            if (e is CopyContentEventArgs) _mediator.Publish(CopyingContent.FromContentEventArgs(e)).Wait();

            if (e is SaveContentEventArgs) _mediator.Publish(CreatingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnDeletingContentVersion(object sender, ContentEventArgs e)
        {
            _mediator.Publish(DeletingContentVersion.FromContentEventArgs(e)).Wait();
        }

        private void OnDeletedContentVersion(object sender, ContentEventArgs e)
        {
            _mediator.Publish(DeletedContentVersion.FromContentEventArgs(e)).Wait();
        }

        private void OnSavingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(SavingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnSavedContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(SavedContent.FromContentEventArgs(e)).Wait();
        }

        private void OnMovingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(MovingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnMovedContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(MovedContent.FromContentEventArgs(e)).Wait();
        }

        private void OnDeletingContentLanguage(object sender, ContentEventArgs e)
        {
            _mediator.Publish(DeletingContentLanguage.FromContentEventArgs(e)).Wait();
        }

        private void OnDeletedContentLanguage(object sender, ContentEventArgs e)
        {
            _mediator.Publish(DeletedContentLanguage.FromContentEventArgs(e)).Wait();
        }

        private void OnCreatingContentLanguage(object sender, ContentEventArgs e)
        {
            _mediator.Publish(CreatingContentLanguage.FromContentEventArgs(e)).Wait();
        }

        private void OnCreatedContentLanguage(object sender, ContentEventArgs e)
        {
            _mediator.Publish(CreatedContentLanguage.FromContentEventArgs(e)).Wait();
        }

        private void OnDeletingContent(object sender, DeleteContentEventArgs e)
        {
            _mediator.Publish(DeletingContent.FromDeleteContentArgs(e)).Wait();
        }

        private void OnDeletedContent(object sender, DeleteContentEventArgs e)
        {
            _mediator.Publish(DeletedContent.FromDeleteContentArgs(e)).Wait();
        }

        private void OnSchedulingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(SchedulingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnScheduledContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(ScheduledContent.FromContentEventArgs(e)).Wait();
        }

        private void OnCheckingOutContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(CheckingOutContent.FromContentEventArgs(e)).Wait();
        }

        private void OnCheckedOutContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(CheckedOutContent.FromContentEventArgs(e)).Wait();
        }

        private void OnRejectingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(RejectingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnRejectedContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(RejectedContent.FromContentEventArgs(e)).Wait();
        }

        private void OnRequestedApproval(object sender, ContentEventArgs e)
        {
            _mediator.Publish(RequestedApproval.FromContentEventArgs(e)).Wait();
        }

        private void OnRequestingApproval(object sender, ContentEventArgs e)
        {
            _mediator.Publish(RequestingApproval.FromContentEventArgs(e)).Wait();
        }

        private void OnPublishedContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(PublishedContent.FromContentEventArgs(e)).Wait();
        }

        private void OnPublishingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(PublishingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnLoadingDefaultContent(object sender, ContentEventArgs e)
        {
            if (LoadingDefaultContent.Match(e)) _mediator.Publish(LoadingDefaultContent.FromContentEventArgs(e)).Wait();
            if (CreatingLanguageBranch.Match(e)) _mediator.Publish(CreatingLanguageBranch.FromContentEventArgs(e)).Wait();
        }

        private void OnLoadedDefaultContent(object sender, ContentEventArgs e)
        {
            if (LoadedDefaultContent.Match(e)) _mediator.Publish(LoadedDefaultContent.FromContentEventArgs(e)).Wait();
            if (CreatedLanguageBranch.Match(e)) _mediator.Publish(CreatedLanguageBranch.FromContentEventArgs(e)).Wait();
        }

        private void OnLoadingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(LoadingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnLoadedContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(LoadedContent.FromContentEventArgs(e)).Wait();
        }

        private void OnFailedLoadingContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(FailedLoadingContent.FromContentEventArgs(e)).Wait();
        }

        private void OnLoadingChildren(object sender, ChildrenEventArgs e)
        {
            _mediator.Publish(LoadingChildren.FromChildrenEventArgs(e)).Wait();
        }

        private void OnLoadedChildren(object sender, ChildrenEventArgs e)
        {
            _mediator.Publish(LoadedChildren.FromChildrenEventArgs(e)).Wait();
        }

        private void OnFailedLoadingChildren(object sender, ChildrenEventArgs e)
        {
            _mediator.Publish(FailedLoadingChildren.FromChildrenEventArgs(e)).Wait();
        }

        private void OnCheckingInContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(CheckingInContent.FromContentEventArgs(e)).Wait();
        }

        private void OnCheckedInContent(object sender, ContentEventArgs e)
        {
            _mediator.Publish(CheckedInContent.FromContentEventArgs(e)).Wait();
        }
    }
}