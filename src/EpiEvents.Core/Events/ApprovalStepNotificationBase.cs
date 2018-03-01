using System;
using EpiEvents.Core.Common;
using MediatR;

namespace EpiEvents.Core.Events
{
    public abstract class ApprovalStepNotificationBase<T> : ValueObject<T>, INotification
        where T : ValueObject<T>
    {
        protected ApprovalStepNotificationBase(
            int stepIndex,
            int approvalId,
            Uri approvalReference,
            string comment,
            int definitionVersionId,
            bool forced,
            string username)
        {
            StepIndex = stepIndex;
            ApprovalId = approvalId;
            ApprovalReference = approvalReference;
            Comment = comment;
            DefinitionVersionId = definitionVersionId;
            Forced = forced;
            Username = username;
        }

        public int StepIndex { get; }
        public int ApprovalId { get; }
        public Uri ApprovalReference { get; }
        public string Comment { get; }
        public int DefinitionVersionId { get; }
        public bool Forced { get; }
        public string Username { get; }
    }
}