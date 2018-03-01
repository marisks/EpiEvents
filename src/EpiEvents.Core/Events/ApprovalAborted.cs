using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalAborted : ApprovalNotificationBase<ApprovalAborted>
    {
        private ApprovalAborted(
            int approvalId,
            Uri approvalReference,
            string comment,
            int definitionVersionId,
            bool forced,
            string username)
            : base(approvalId,
                approvalReference,
                comment,
                definitionVersionId,
                forced,
                username)
        {
        }

        public static ApprovalAborted FromApprovalEventArgs(ApprovalEventArgs args)
        {
            return new ApprovalAborted(
                args.ApprovalID,
                args.ApprovalReference,
                args.Comment,
                args.DefinitionVersionID,
                args.Forced,
                args.Username);
        }
    }
}