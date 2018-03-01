using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalStepRejected : ApprovalStepNotificationBase<ApprovalStepRejected>
    {
        private ApprovalStepRejected(
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

        public static ApprovalStepRejected FromApprovalStepEventArgs(ApprovalStepEventArgs args)
        {
            return new ApprovalStepRejected(
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