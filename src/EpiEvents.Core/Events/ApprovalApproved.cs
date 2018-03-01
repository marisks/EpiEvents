using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalApproved : ApprovalNotificationBase<ApprovalApproved>
    {
        private ApprovalApproved(
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

        public static ApprovalApproved FromApprovalEventArgs(ApprovalEventArgs args)
        {
            return new ApprovalApproved(
                args.ApprovalID,
                args.ApprovalReference,
                args.Comment,
                args.DefinitionVersionID,
                args.Forced,
                args.Username);
        }
    }
}