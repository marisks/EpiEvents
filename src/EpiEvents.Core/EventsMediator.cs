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

        private async void OnCreatingContent(object sender, ContentEventArgs e)
        {
            if(e is CopyContentEventArgs)
            {
                await _mediator.Publish(CopyingContent.FromContentEventArgs(e));
            }

            if(e is SaveContentEventArgs)
            {
                await _mediator.Publish(CreatingContent.FromContentEventArgs(e));
            }
        }

        private async void OnDeletingContentVersion(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(DeletingContentVersion.FromContentEventArgs(e));
        }

        private async void OnDeletedContentVersion(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(DeletedContentVersion.FromContentEventArgs(e));
        }

        private async void OnSavingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(SavingContent.FromContentEventArgs(e));
        }

        private async void OnSavedContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(SavedContent.FromContentEventArgs(e));
        }

        private async void OnMovingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(MovingContent.FromContentEventArgs(e));
        }

        private async void OnMovedContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(MovedContent.FromContentEventArgs(e));
        }

        private async void OnDeletingContentLanguage(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(DeletingContentLanguage.FromContentEventArgs(e));
        }

        private async void OnDeletedContentLanguage(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(DeletedContentLanguage.FromContentEventArgs(e));
        }

        private async void OnCreatingContentLanguage(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(CreatingContentLanguage.FromContentEventArgs(e));
        }

        private async void OnCreatedContentLanguage(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(CreatedContentLanguage.FromContentEventArgs(e));
        }

        private async void OnDeletingContent(object sender, DeleteContentEventArgs e)
        {
            await _mediator.Publish(DeletingContent.FromDeleteContentArgs(e));
        }

        private async void OnDeletedContent(object sender, DeleteContentEventArgs e)
        {
            await _mediator.Publish(DeletedContent.FromDeleteContentArgs(e));
        }

        private async void OnSchedulingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(SchedulingContent.FromContentEventArgs(e));
        }

        private async void OnScheduledContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(ScheduledContent.FromContentEventArgs(e));
        }

        private async void OnCheckingOutContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(CheckingOutContent.FromContentEventArgs(e));
        }

        private async void OnCheckedOutContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(CheckedOutContent.FromContentEventArgs(e));
        }

        private async void OnRejectingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(RejectingContent.FromContentEventArgs(e));
        }

        private async void OnRejectedContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(RejectedContent.FromContentEventArgs(e));
        }

        private async void OnRequestedApproval(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(RequestedApproval.FromContentEventArgs(e));
        }

        private async void OnRequestingApproval(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(RequestingApproval.FromContentEventArgs(e));
        }

        private async void OnPublishedContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(PublishedContent.FromContentEventArgs(e));
        }

        private async void OnPublishingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(PublishingContent.FromContentEventArgs(e));
        }

        private async void OnLoadingDefaultContent(object sender, ContentEventArgs e)
        {
            if (LoadingDefaultContent.Match(e))
            {
                await _mediator.Publish(LoadingDefaultContent.FromContentEventArgs(e));
            }
            if (CreatingLanguageBranch.Match(e))
            {
                await _mediator.Publish(CreatingLanguageBranch.FromContentEventArgs(e));
            }
        }

        private async void OnLoadedDefaultContent(object sender, ContentEventArgs e)
        {
            if (LoadedDefaultContent.Match(e))
            {
                await _mediator.Publish(LoadedDefaultContent.FromContentEventArgs(e));
            }
            if (CreatedLanguageBranch.Match(e))
            {
                await _mediator.Publish(CreatedLanguageBranch.FromContentEventArgs(e));
            }
        }

        private async void OnLoadingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(LoadingContent.FromContentEventArgs(e));
        }

        private async void OnLoadedContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(LoadedContent.FromContentEventArgs(e));
        }

        private async void OnFailedLoadingContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(FailedLoadingContent.FromContentEventArgs(e));
        }

        private async void OnLoadingChildren(object sender, ChildrenEventArgs e)
        {
            await _mediator.Publish(LoadingChildren.FromChildrenEventArgs(e));
        }

        private async void OnLoadedChildren(object sender, ChildrenEventArgs e)
        {
            await _mediator.Publish(LoadedChildren.FromChildrenEventArgs(e));
        }

        private async void OnFailedLoadingChildren(object sender, ChildrenEventArgs e)
        {
            await _mediator.Publish(FailedLoadingChildren.FromChildrenEventArgs(e));
        }

        private async void OnCheckingInContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(CheckingInContent.FromContentEventArgs(e));
        }

        private async void OnCheckedInContent(object sender, ContentEventArgs e)
        {
            await _mediator.Publish(CheckedInContent.FromContentEventArgs(e));
        }
    }
}