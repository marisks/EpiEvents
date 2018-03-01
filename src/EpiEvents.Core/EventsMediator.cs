using System;
using EpiEvents.Core.Common;
using EpiEvents.Core.Events;
using EPiServer;
using EPiServer.Approvals;
using EPiServer.Core;
using MediatR;

namespace EpiEvents.Core
{
    public class EventsMediator
    {
        private readonly IApprovalEngineEvents _approvalEngineEvents;
        private readonly ISettings _settings;
        private readonly IContentEvents _contentEvents;
        private readonly IMediator _mediator;

        public EventsMediator(IContentEvents contentEvents, IApprovalEngineEvents approvalEngineEvents, IMediator mediator, ISettings settings)
        {
            _approvalEngineEvents = approvalEngineEvents ?? throw new ArgumentNullException(nameof(approvalEngineEvents));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _contentEvents = contentEvents ?? throw new ArgumentNullException(nameof(contentEvents));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void Initialize()
        {
            if (_settings.EnableLoadingEvents)
            {
                _contentEvents.LoadingChildren += OnLoadingChildren;
                _contentEvents.LoadedChildren += OnLoadedChildren;
                _contentEvents.FailedLoadingChildren += OnFailedLoadingChildren;
                _contentEvents.LoadingContent += OnLoadingContent;
                _contentEvents.LoadedContent += OnLoadedContent;
                _contentEvents.FailedLoadingContent += OnFailedLoadingContent;
            }

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
            _contentEvents.CreatedContent += OnCreatedContent;

            _approvalEngineEvents.StepStarted += OnStepStarted;
        }

        private void OnCreatedContent(object sender, ContentEventArgs e)
        {
            if (e is CopyContentEventArgs) AsyncHelper.RunSync(() => _mediator.Publish(CopyiedContent.FromContentEventArgs(e)));
            if (e is SaveContentEventArgs) AsyncHelper.RunSync(() => _mediator.Publish(CreatedContent.FromContentEventArgs(e)));
        }

        private void OnCreatingContent(object sender, ContentEventArgs e)
        {
            if (e is CopyContentEventArgs) AsyncHelper.RunSync(() => _mediator.Publish(CopyingContent.FromContentEventArgs(e)));
            if (e is SaveContentEventArgs) AsyncHelper.RunSync(() => _mediator.Publish(CreatingContent.FromContentEventArgs(e)));
        }

        private void OnDeletingContentVersion(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(DeletingContentVersion.FromContentEventArgs(e)));
        }

        private void OnDeletedContentVersion(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(DeletedContentVersion.FromContentEventArgs(e)));
        }

        private void OnSavingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(SavingContent.FromContentEventArgs(e)));
        }

        private void OnSavedContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(SavedContent.FromContentEventArgs(e)));
        }

        private void OnMovingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(MovingContent.FromContentEventArgs(e)));
        }

        private void OnMovedContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(MovedContent.FromContentEventArgs(e)));
        }

        private void OnDeletingContentLanguage(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(DeletingContentLanguage.FromContentEventArgs(e)));
        }

        private void OnDeletedContentLanguage(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(DeletedContentLanguage.FromContentEventArgs(e)));
        }

        private void OnCreatingContentLanguage(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(CreatingContentLanguage.FromContentEventArgs(e)));
        }

        private void OnCreatedContentLanguage(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(CreatedContentLanguage.FromContentEventArgs(e)));
        }

        private void OnDeletingContent(object sender, DeleteContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(DeletingContent.FromDeleteContentArgs(e)));
        }

        private void OnDeletedContent(object sender, DeleteContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(DeletedContent.FromDeleteContentArgs(e)));
        }

        private void OnSchedulingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(SchedulingContent.FromContentEventArgs(e)));
        }

        private void OnScheduledContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(ScheduledContent.FromContentEventArgs(e)));
        }

        private void OnCheckingOutContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(CheckingOutContent.FromContentEventArgs(e)));
        }

        private void OnCheckedOutContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(CheckedOutContent.FromContentEventArgs(e)));
        }

        private void OnRejectingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(RejectingContent.FromContentEventArgs(e)));
        }

        private void OnRejectedContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(RejectedContent.FromContentEventArgs(e)));
        }

        private void OnRequestedApproval(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(RequestedApproval.FromContentEventArgs(e)));
        }

        private void OnRequestingApproval(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(RequestingApproval.FromContentEventArgs(e)));
        }

        private void OnPublishedContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(PublishedContent.FromContentEventArgs(e)));
        }

        private void OnPublishingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(PublishingContent.FromContentEventArgs(e)));
        }

        private void OnLoadingDefaultContent(object sender, ContentEventArgs e)
        {
            if (LoadingDefaultContent.Match(e)) AsyncHelper.RunSync(() => _mediator.Publish(LoadingDefaultContent.FromContentEventArgs(e)));
            if (CreatingLanguageBranch.Match(e)) AsyncHelper.RunSync(() => _mediator.Publish(CreatingLanguageBranch.FromContentEventArgs(e)));
        }

        private void OnLoadedDefaultContent(object sender, ContentEventArgs e)
        {
            if (LoadedDefaultContent.Match(e)) AsyncHelper.RunSync(() => _mediator.Publish(LoadedDefaultContent.FromContentEventArgs(e)));
            if (CreatedLanguageBranch.Match(e)) AsyncHelper.RunSync(() => _mediator.Publish(CreatedLanguageBranch.FromContentEventArgs(e)));
        }

        private void OnLoadingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(LoadingContent.FromContentEventArgs(e)));
        }

        private void OnLoadedContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(LoadedContent.FromContentEventArgs(e)));
        }

        private void OnFailedLoadingContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(FailedLoadingContent.FromContentEventArgs(e)));
        }

        private void OnLoadingChildren(object sender, ChildrenEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(LoadingChildren.FromChildrenEventArgs(e)));
        }

        private void OnLoadedChildren(object sender, ChildrenEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(LoadedChildren.FromChildrenEventArgs(e)));
        }

        private void OnFailedLoadingChildren(object sender, ChildrenEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(FailedLoadingChildren.FromChildrenEventArgs(e)));
        }

        private void OnCheckingInContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(CheckingInContent.FromContentEventArgs(e)));
        }

        private void OnCheckedInContent(object sender, ContentEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(CheckedInContent.FromContentEventArgs(e)));
        }

        private void OnStepStarted(ApprovalStepEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(StepStarted.FromApprovalStepEventArgs(e)));
        }
    }
}