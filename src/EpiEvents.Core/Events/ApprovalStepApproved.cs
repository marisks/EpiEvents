using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalStepApproved : ApprovalStepNotificationBase<ApprovalStepApproved>
    {
        private ApprovalStepApproved(
            int stepIndex,
            int approvalId,
            Uri approvalReference,
            string comment,
            int definitionVersionId,
            bool forced,
            string username)
            : base(stepIndex,
                approvalId,
                approvalReference,
                comment,
                definitionVersionId,
                forced,
                username)
        {
        }

        public static ApprovalStepApproved FromApprovalStepEventArgs(ApprovalStepEventArgs args)
        {
            return new ApprovalStepApproved(
                args.StepIndex,
                args.ApprovalID,
                args.ApprovalReference,
                args.Comment,
                args.DefinitionVersionID,
                args.Forced,
                args.Username);
        }
    }
}