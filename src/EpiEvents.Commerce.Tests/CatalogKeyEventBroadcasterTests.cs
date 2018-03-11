using System.Linq;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using EpiEvents.Commerce.Events;
using EpiEvents.Core.Tests.Base;
using FakeItEasy;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Engine.Events;
using MediatR;

namespace EpiEvents.Commerce.Tests
{
    public class CatalogKeyEventBroadcasterTests
    {
        private readonly CatalogKeyEventBroadcaster _catalogKeyEventBroadcaster;
        private readonly IMediator _mediator;
        private readonly Fixture _fixture;

        public CatalogKeyEventBroadcasterTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoFakeItEasyCustomization());
            _catalogKeyEventBroadcaster = new CatalogKeyEventBroadcaster();
            _mediator = A.Fake<IMediator>();
            var eventsMediator = new EventsMediator(_catalogKeyEventBroadcaster, _mediator);
            eventsMediator.Initialize();
        }

        public void it_publishes_InventoryUpdated_notification()
        {
            var keys = _fixture.CreateMany<CatalogKey>().ToArray();
            var args = new InventoryUpdateEventArgs(keys);
            var expected = InventoryUpdated.FromInventoryUpdateEventArgs(args);

            _catalogKeyEventBroadcaster.OnInventoryUpdated(this, args);

            _mediator.ShouldPublishNotificationWith<InventoryUpdated>(actual => actual == expected);
        }
    }
}