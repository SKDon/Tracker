﻿using Tracker.Core.Contracts.Calculation;
using Tracker.Core.Contracts.Event;
using Tracker.DataAccess.Contracts.Contracts.Calculation;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories;

namespace Tracker.Core.Calculation
{
	public sealed class CalculationServiceWithEvent : ICalculationService
	{
		private readonly ICalculationRepository _calculations;
		private readonly IEventFacade _events;
		private readonly ICalculationService _service;

		public CalculationServiceWithEvent(
			ICalculationService service,
			IEventFacade events,
			ICalculationRepository calculations)
		{
			_service = service;
			_events = events;
			_calculations = calculations;
		}

		public CalculationData Calculate(long applicationId)
		{
			var data = _service.Calculate(applicationId);

			_events.Add(applicationId, EventType.Calculate, EventState.Emailing, data);

			return data;
		}

		public void CancelCalculatation(long applicationId)
		{
			var data = _calculations.GetByApplication(applicationId);

			_service.CancelCalculatation(applicationId);

			_events.Add(applicationId, EventType.CalculationCanceled, EventState.Emailing, data);
		}
	}
}