using EpiEvents.Core.Common;
using Mediachase.Commerce.Engine.Events;
using MediatR;

namespace EpiEvents.Commerce.Events
{
    public class InventoryUpdated : ValueObject<InventoryUpdated>, INotification
    {
        public static InventoryUpdated FromInventoryUpdateEventArgs(InventoryUpdateEventArgs args)
        {
            return new InventoryUpdated();
        }
    }
}