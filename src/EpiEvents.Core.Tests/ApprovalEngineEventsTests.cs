using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using EpiEvents.Core.Common;
using EpiEvents.Core.Events;
using EPiServer.Approvals;
using EPiServer.Core;
using FakeItEasy;
using MediatR;

namespace EpiEvents.Core.Tests
{
    public class ApprovalEngineEventsTests
    {

        private readonly IApprovalEngineEvents _approvalEngineEvents;
        private readonly IMediator _mediator;

        public ApprovalEngineEventsTests()
        {
            var contentEvents = FakeItEasy.A.Fake<IContentEvents>();
            _approvalEngineEvents = FakeItEasy.A.Fake<IApprovalEngineEvents>();
            _mediator = FakeItEasy.A.Fake<IMediator>();
            var settings = FakeItEasy.A.Fake<ISettings>();

            var eventsMediator = new EventsMediator(contentEvents, _approvalEngineEvents, _mediator, settings);

            eventsMediator.Initialize();
        }

        [FactoryMethodData(typeof(ApprovalEngineEventsTests), nameof(GetParameters))]
        public void it_publishes_notification_from_event<TEvent, TNotification>(
            TEvent ev, TNotification expected, Action<IApprovalEngineEvents, TEvent> raise)
            where TNotification : ValueObject<TNotification>, INotification
        {
            raise(_approvalEngineEvents, ev);

            _mediator.ShouldPublishNotificationWith<TNotification>(actual => actual == expected);
        }

        public static IEnumerable<object[]> GetParameters()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoFakeItEasyCustomization());

            foreach (var parameters in FromApprovalStepEventArgs(fixture)) yield return parameters;
            foreach (var parameters in FromApprovalEventArgs(fixture)) yield return parameters;
        }

        private static IEnumerable<object[]> FromApprovalStepEventArgs(Fixture fixture)
        {
            var args = fixture.Create<ApprovalStepEventArgs>();

            yield return new[]
            {
                args,
                ApprovalStepStarted.FromApprovalStepEventArgs(args),
                A<IApprovalEngineEvents, ApprovalStepEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.StepStarted += Raise.FreeForm<ApprovalStepEventHandler>.With(e))
            };

            yield return new[]
            {
                args,
                ApprovalStepApproved.FromApprovalStepEventArgs(args),
                A<IApprovalEngineEvents, ApprovalStepEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.StepApproved += Raise.FreeForm<ApprovalStepEventHandler>.With(e))
            };

            yield return new[]
            {
                args,
                ApprovalStepRejected.FromApprovalStepEventArgs(args),
                A<IApprovalEngineEvents, ApprovalStepEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.StepRejected += Raise.FreeForm<ApprovalStepEventHandler>.With(e))
            };
        }

        private static IEnumerable<object[]> FromApprovalEventArgs(Fixture fixture)
        {
            var args = fixture.Create<ApprovalEventArgs>();

            yield return new[]
            {
                args,
                ApprovalStarted.FromApprovalEventArgs(args),
                A<IApprovalEngineEvents, ApprovalEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.Started += Raise.FreeForm<ApprovalEventHandler>.With(e))
            };

            yield return new[]
            {
                args,
                ApprovalAborted.FromApprovalEventArgs(args),
                A<IApprovalEngineEvents, ApprovalEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.Aborted += Raise.FreeForm<ApprovalEventHandler>.With(e))
            };

            yield return new[]
            {
                args,
                ApprovalApproved.FromApprovalEventArgs(args),
                A<IApprovalEngineEvents, ApprovalEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.Approved += Raise.FreeForm<ApprovalEventHandler>.With(e))
            };

            yield return new[]
            {
                args,
                ApprovalRejected.FromApprovalEventArgs(args),
                A<IApprovalEngineEvents, ApprovalEventArgs>(
                    (approvalEngineEvents, e) => approvalEngineEvents.Rejected += Raise.FreeForm<ApprovalEventHandler>.With(e))
            };
        }

        private static object A<T1, T2>(Action<T1, T2> action)
        {
            return action;
        }
    }
}