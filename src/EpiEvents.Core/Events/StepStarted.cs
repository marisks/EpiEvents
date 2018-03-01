using System;
using EPiServer.Approvals;

namespace EpiEvents.Core.Events
{
    public class StepStarted : ApprovalStepNotificationBase<StepStarted>
    {
        private StepStarted(
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

        public static StepStarted FromApprovalStepEventArgs(ApprovalStepEventArgs args)
        {
            return new StepStarted(
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