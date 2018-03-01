using System;
using EpiEvents.Core.Common;
using MediatR;

namespace EpiEvents.Core.Events
{
    public abstract class ApprovalNotificationBase<T> : ValueObject<T>, INotification
        where T : ValueObject<T>
    {
        protected ApprovalNotificationBase(
            int approvalId,
            Uri approvalReference,
            string comment,
            int definitionVersionId,
            bool forced,
            string username)
        {
            ApprovalId = approvalId;
            ApprovalReference = approvalReference;
            Comment = comment;
            DefinitionVersionId = definitionVersionId;
            Forced = forced;
            Username = username;
        }

        public int ApprovalId { get; }
        public Uri ApprovalReference { get; }
        public string Comment { get; }
        public int DefinitionVersionId { get; }
        public bool Forced { get; }
        public string Username { get; }
    }
}