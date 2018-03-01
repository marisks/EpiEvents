using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalStarted : ApprovalNotificationBase<ApprovalStarted>
    {
        private ApprovalStarted(
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

        public static ApprovalStarted FromApprovalEventArgs(ApprovalEventArgs args)
        {
            return new ApprovalStarted(
                args.ApprovalID,
                args.ApprovalReference,
                args.Comment,
                args.DefinitionVersionID,
                args.Forced,
                args.Username);
        }
    }
}