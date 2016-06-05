using System.Linq;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Exceptions;
using Tracker.Core.Contracts.State;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.Utilities;

namespace Tracker.Core.State
{
	public sealed class ApplicationStateManager : IApplicationStateManager
	{
		private readonly IStateConfig _config;
		private readonly IApplicationEditor _editor;
		private readonly IIdentityService _identity;
		private readonly IStateSettingsRepository _settings;

		public ApplicationStateManager(
			IStateConfig config,
			IApplicationEditor editor,
			IIdentityService identity,
			IStateSettingsRepository settings)
		{
			_config = config;
			_editor = editor;
			_identity = identity;
			_settings = settings;
		}

		public void SetState(long applicationId, long stateId)
		{
			if(!HasPermissionToSetState(stateId))
				throw new AccessForbiddenException("User don't have access to the state " + stateId);

			// todo: 2. test logic with states (260)
			if(stateId == _config.CargoInStockStateId)
			{
				_editor.SetDateInStock(applicationId, DateTimeProvider.Now);
			}

			_editor.SetState(applicationId, stateId);
		}

		private bool HasPermissionToSetState(long stateId)
		{
			return _settings.GetStateAvailabilities()
				.Any(x => x.StateId == stateId && _identity.IsInRole(x.Role));
		}
	}
}