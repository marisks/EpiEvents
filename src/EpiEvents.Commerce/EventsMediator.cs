using System;
using EpiEvents.Commerce.Events;
using EpiEvents.Core.Common;
using Mediachase.Commerce.Engine.Events;
using MediatR;

namespace EpiEvents.Commerce
{
    public class EventsMediator
    {
        private readonly IMediator _mediator;
        private readonly CatalogKeyEventBroadcaster _catalogKeyEventBroadcaster;

        public EventsMediator(CatalogKeyEventBroadcaster catalogKeyEventBroadcaster, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _catalogKeyEventBroadcaster = catalogKeyEventBroadcaster ?? throw new ArgumentNullException(nameof(catalogKeyEventBroadcaster));
        }

        public void Initialize()
        {
            _catalogKeyEventBroadcaster.InventoryUpdated += OnInventoryUpdated;
            _catalogKeyEventBroadcaster.PriceUpdated += OnPriceUpdated;
        }

        private void OnInventoryUpdated(object sender, InventoryUpdateEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(InventoryUpdated.FromInventoryUpdateEventArgs(e)));
        }

        private void OnPriceUpdated(object sender, PriceUpdateEventArgs e)
        {
            AsyncHelper.RunSync(() => _mediator.Publish(PriceUpdated.FromPriceUpdateEventArgs(e)));
        }
    }
}