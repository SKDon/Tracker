﻿using System.Globalization;
using System.Linq;
using Tracker.Core.Contracts.Exceptions;
using Tracker.Core.Contracts.State;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.Services.Abstract;
using Tracker.ViewModels.AirWaybill;

namespace Tracker.Services.AirWaybill
{
	internal sealed class AwbUpdateManager : IAwbUpdateManager
	{
		private readonly IAwbRepository _awbs;
		private readonly IStateConfig _config;

		public AwbUpdateManager(
			IAwbRepository awbs,
			IStateConfig config)
		{
			_awbs = awbs;
			_config = config;
		}

		public void Update(long id, AwbAdminModel model)
		{
			var data = _awbs.Get(id).First();

			AwbMapper.Map(model, data);

			_awbs.Update(id, data);
		}

		public void Update(long id, AwbBrokerModel model)
		{
			var data = _awbs.Get(id).First();

			if(data.StateId == _config.CargoIsCustomsClearedStateId)
			{
				throw new UnexpectedStateException(
					data.StateId,
					"Can't update an AWB while it has the state "
					+ _config.CargoIsCustomsClearedStateId.ToString(CultureInfo.InvariantCulture));
			}

			AwbMapper.Map(model, data);

			_awbs.Update(id, data);
		}

		public void Update(long id, AwbSenderModel model)
		{
			var data = _awbs.Get(id).First();

			AwbMapper.Map(model, data);

			_awbs.Update(id, data);
		}

		public void SetAdditionalCost(long awbId, decimal? additionalCost)
		{
			_awbs.SetAdditionalCost(awbId, additionalCost);
		}
	}
}