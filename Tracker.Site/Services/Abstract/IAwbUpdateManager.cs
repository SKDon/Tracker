﻿using Tracker.ViewModels.AirWaybill;

namespace Tracker.Services.Abstract
{
	public interface IAwbUpdateManager
	{
		void Update(long id, AwbAdminModel model);
		void Update(long id, AwbBrokerModel model);
		void Update(long id, AwbSenderModel model);
		void SetAdditionalCost(long awbId, decimal? additionalCost);
	}
}
