using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Engine.Events;
using MediatR;

namespace EpiEvents.Commerce.Events
{
    public class PriceUpdated : ValueObject<PriceUpdated>, INotification
    {
        public IEnumerable<CatalogKey> CatalogKeys { get; }

        public PriceUpdated(IEnumerable<CatalogKey> catalogKeys)
        {
            CatalogKeys = catalogKeys ?? throw new ArgumentNullException(nameof(catalogKeys));
        }

        public static PriceUpdated FromPriceUpdateEventArgs(PriceUpdateEventArgs args)
        {
            return new PriceUpdated(args.CatalogKeys);
        }
    }
}