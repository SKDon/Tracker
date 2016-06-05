using System.Linq;
using Tracker.Core.Contracts.Event;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Services.Abstract;
using Tracker.ViewModels;

namespace Tracker.Services
{
	internal sealed class TransitService : ITransitService
	{
		private readonly ICarrierRepository _carriers;
		private readonly IEventFacade _events;
		private readonly ITransitRepository _transits;

		public TransitService(ITransitRepository transits, ICarrierRepository carriers, IEventFacade events)
		{
			_transits = transits;
			_carriers = carriers;
			_events = events;
		}

		public void Update(long transitId, TransitEditModel transit, long? forsedCarrierId, long? applicationId)
		{
			var data = _transits.Get(transitId).Single();

			var carrierId = GetCarrier(forsedCarrierId, transit.CityId, data.CarrierId);

			if(applicationId.HasValue && data.CarrierId != carrierId)
			{
				_events.Add(applicationId.Value, EventType.SetCarrier, EventState.Emailing);
			}

			TransitMapper.Map(transit, data, carrierId);

			_transits.Update(data);
		}

		public void Delete(long transitId)
		{
			_transits.Delete(transitId);
		}

		public long Add(TransitEditModel transit, long? forsedCarrierId)
		{
			var data = new TransitData();

			TransitMapper.Map(transit, data, GetCarrier(forsedCarrierId, transit.CityId, null));

			var transitId = _transits.Add(data);

			return transitId;
		}

		private long GetByCityOrAny(long cityId, long? oldCarrierId)
		{
			var inCity = _carriers.GetByCity(cityId);

			if(inCity.Length == 0)
			{
				return oldCarrierId ?? _carriers.GetAll().Select(x => x.Id).First();
			}

			if(oldCarrierId.HasValue && inCity.Contains(oldCarrierId.Value))
			{
				return oldCarrierId.Value;
			}

			return inCity.First();
		}

		private long GetCarrier(long? carrierId, long cityId, long? oldCarrierId)
		{
			return carrierId.HasValue
				? carrierId.Value
				: GetByCityOrAny(cityId, oldCarrierId);
		}
	}
}