using System;
using System.Collections.Generic;
using EpiEvents.Core.Common;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Engine.Events;
using MediatR;

namespace EpiEvents.Commerce.Events
{
    public class InventoryUpdated : ValueObject<InventoryUpdated>, INotification
    {
        public IEnumerable<CatalogKey> CatalogKeys { get; }

        public InventoryUpdated(IEnumerable<CatalogKey> catalogKeys)
        {
            CatalogKeys = catalogKeys ?? throw new ArgumentNullException(nameof(catalogKeys));
        }

        public static InventoryUpdated FromInventoryUpdateEventArgs(InventoryUpdateEventArgs args)
        {
            return new InventoryUpdated(args.CatalogKeys);
        }
    }
}