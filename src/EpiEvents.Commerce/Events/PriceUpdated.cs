using EpiEvents.Core.Common;
using Mediachase.Commerce.Engine.Events;
using MediatR;

namespace EpiEvents.Commerce.Events
{
    public class PriceUpdated : ValueObject<PriceUpdated>, INotification
    {
        public static PriceUpdated FromPriceUpdateEventArgs(PriceUpdateEventArgs args)
        {
            return new PriceUpdated();
        }
    }
}