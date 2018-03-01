using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalRejected : ApprovalNotificationBase<ApprovalRejected>
    {
        private ApprovalRejected(
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

        public static ApprovalRejected FromApprovalEventArgs(ApprovalEventArgs args)
        {
            return new ApprovalRejected(
                args.ApprovalID,
                args.ApprovalReference,
                args.Comment,
                args.DefinitionVersionID,
                args.Forced,
                args.Username);
        }
    }
}