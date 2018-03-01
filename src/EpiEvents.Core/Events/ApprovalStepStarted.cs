using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class ApprovalStepStarted : ApprovalStepNotificationBase<ApprovalStepStarted>
    {
        private ApprovalStepStarted(
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

        public static ApprovalStepStarted FromApprovalStepEventArgs(ApprovalStepEventArgs args)
        {
            return new ApprovalStepStarted(
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