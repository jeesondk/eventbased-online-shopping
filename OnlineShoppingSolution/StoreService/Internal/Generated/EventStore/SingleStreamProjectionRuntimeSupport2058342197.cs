// <auto-generated/>
#pragma warning disable
using Marten;
using Marten.Events.Aggregation;
using Marten.Internal.Storage;
using System;
using System.Linq;

namespace Marten.Generated.EventStore
{
    // START: SingleStreamProjectionLiveAggregation2058342197
    public class SingleStreamProjectionLiveAggregation2058342197 : Marten.Events.Aggregation.SyncLiveAggregatorBase<Common.Domain.Shop.Aggregates.ShopSessionAggregate>
    {
        private readonly Marten.Events.Aggregation.SingleStreamProjection<Common.Domain.Shop.Aggregates.ShopSessionAggregate> _singleStreamProjection;

        public SingleStreamProjectionLiveAggregation2058342197(Marten.Events.Aggregation.SingleStreamProjection<Common.Domain.Shop.Aggregates.ShopSessionAggregate> singleStreamProjection)
        {
            _singleStreamProjection = singleStreamProjection;
        }



        public override Common.Domain.Shop.Aggregates.ShopSessionAggregate Build(System.Collections.Generic.IReadOnlyList<Marten.Events.IEvent> events, Marten.IQuerySession session, Common.Domain.Shop.Aggregates.ShopSessionAggregate snapshot)
        {
            if (!events.Any()) return null;
            Common.Domain.Shop.Aggregates.ShopSessionAggregate shopSessionAggregate = null;
            var usedEventOnCreate = snapshot is null;
            snapshot ??= Create(events[0], session);;
            if (snapshot is null)
            {
                usedEventOnCreate = false;
                snapshot = CreateDefault(events[0]);
            }

            foreach (var @event in events.Skip(usedEventOnCreate ? 1 : 0))
            {
                snapshot = Apply(@event, snapshot, session);
            }

            return snapshot;
        }


        public Common.Domain.Shop.Aggregates.ShopSessionAggregate Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            return null;
        }


        public Common.Domain.Shop.Aggregates.ShopSessionAggregate CreateDefault(Marten.Events.IEvent @event)
        {
            return new Common.Domain.Shop.Aggregates.ShopSessionAggregate();
        }


        public Common.Domain.Shop.Aggregates.ShopSessionAggregate Apply(Marten.Events.IEvent @event, Common.Domain.Shop.Aggregates.ShopSessionAggregate aggregate, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<Common.Domain.Shop.Events.AddItem> event_AddItem1:
                    aggregate.Apply(event_AddItem1.Data);
                    break;
                case Marten.Events.IEvent<Common.Domain.Shop.Events.RemoveItem> event_RemoveItem2:
                    aggregate.Apply(event_RemoveItem2.Data);
                    break;
            }

            return aggregate;
        }

    }

    // END: SingleStreamProjectionLiveAggregation2058342197
    
    
    // START: SingleStreamProjectionInlineHandler2058342197
    public class SingleStreamProjectionInlineHandler2058342197 : Marten.Events.Aggregation.AggregationRuntime<Common.Domain.Shop.Aggregates.ShopSessionAggregate, System.Guid>
    {
        private readonly Marten.IDocumentStore _store;
        private readonly Marten.Events.Aggregation.IAggregateProjection _projection;
        private readonly Marten.Events.Aggregation.IEventSlicer<Common.Domain.Shop.Aggregates.ShopSessionAggregate, System.Guid> _slicer;
        private readonly Marten.Internal.Storage.IDocumentStorage<Common.Domain.Shop.Aggregates.ShopSessionAggregate, System.Guid> _storage;
        private readonly Marten.Events.Aggregation.SingleStreamProjection<Common.Domain.Shop.Aggregates.ShopSessionAggregate> _singleStreamProjection;

        public SingleStreamProjectionInlineHandler2058342197(Marten.IDocumentStore store, Marten.Events.Aggregation.IAggregateProjection projection, Marten.Events.Aggregation.IEventSlicer<Common.Domain.Shop.Aggregates.ShopSessionAggregate, System.Guid> slicer, Marten.Internal.Storage.IDocumentStorage<Common.Domain.Shop.Aggregates.ShopSessionAggregate, System.Guid> storage, Marten.Events.Aggregation.SingleStreamProjection<Common.Domain.Shop.Aggregates.ShopSessionAggregate> singleStreamProjection) : base(store, projection, slicer, storage)
        {
            _store = store;
            _projection = projection;
            _slicer = slicer;
            _storage = storage;
            _singleStreamProjection = singleStreamProjection;
        }



        public override async System.Threading.Tasks.ValueTask<Common.Domain.Shop.Aggregates.ShopSessionAggregate> ApplyEvent(Marten.IQuerySession session, Marten.Events.Projections.EventSlice<Common.Domain.Shop.Aggregates.ShopSessionAggregate, System.Guid> slice, Marten.Events.IEvent evt, Common.Domain.Shop.Aggregates.ShopSessionAggregate aggregate, System.Threading.CancellationToken cancellationToken)
        {
            switch (evt)
            {
                case Marten.Events.IEvent<Common.Domain.Shop.Events.RemoveItem> event_RemoveItem4:
                    aggregate ??= new Common.Domain.Shop.Aggregates.ShopSessionAggregate();
                    aggregate.Apply(event_RemoveItem4.Data);
                    return aggregate;
                case Marten.Events.IEvent<Common.Domain.Shop.Events.AddItem> event_AddItem3:
                    aggregate ??= new Common.Domain.Shop.Aggregates.ShopSessionAggregate();
                    aggregate.Apply(event_AddItem3.Data);
                    return aggregate;
            }

            return aggregate;
        }


        public Common.Domain.Shop.Aggregates.ShopSessionAggregate Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            return null;
        }


        public Common.Domain.Shop.Aggregates.ShopSessionAggregate CreateDefault(Marten.Events.IEvent @event)
        {
            return new Common.Domain.Shop.Aggregates.ShopSessionAggregate();
        }

    }

    // END: SingleStreamProjectionInlineHandler2058342197
    
    
}

