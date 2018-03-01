using System;
using System.Linq.Expressions;
using System.Threading;
using FakeItEasy;
using MediatR;

namespace EpiEvents.Core.Tests
{
    public static class MediatorAssertionExtensions
    {
        public static void ShouldPublishNotificationWith<T>(this IMediator mediator, Expression<Func<T, bool>> predicate)
            where T : INotification
        {
            A.CallTo(() => mediator.Publish(A<T>.That.Matches(predicate), A<CancellationToken>.Ignored)).MustHaveHappened();
        }

        public static void ShouldNotPublishNotificationWith<T>(this IMediator mediator)
            where T : INotification
        {
            FakeItEasy.A.CallTo(() => mediator.Publish(A<T>.Ignored, A<CancellationToken>.Ignored)).MustNotHaveHappened();
        }
    }
}