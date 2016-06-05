using System.Linq;
using Tracker.Core.Contracts.AirWaybill;
using Tracker.Core.Contracts.Event;
using Tracker.Core.Contracts.State;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.Application;

namespace Tracker.Services.Application
{
	public sealed class ApplicationAwbManager : IApplicationAwbManager
	{
		private readonly IAwbRepository _awbs;
		private readonly IStateConfig _config;
		private readonly IApplicationEditor _editor;
		private readonly IEventFacade _events;
		private readonly IApplicationStateManager _states;

		public ApplicationAwbManager(
			IAwbRepository awbs,
			IStateConfig config,
			IEventFacade events,
			IApplicationEditor editor,
			IApplicationStateManager states)
		{
			_awbs = awbs;
			_config = config;
			_events = events;
			_editor = editor;
			_states = states;
		}

		public void SetAwb(long applicationId, long? awbId)
		{
			if(awbId.HasValue)
			{
				var aggregate = _awbs.GetAggregate(new[] { awbId.Value }).First();

				_editor.SetAirWaybill(applicationId, awbId.Value);

				_states.SetState(applicationId, aggregate.StateId);

				_events.Add(applicationId, EventType.SetAwb, EventState.Emailing, awbId.Value);
			}
			else
			{
				_editor.SetAirWaybill(applicationId, null);

				_states.SetState(applicationId, _config.CargoInStockStateId);
			}
		}
	}
}