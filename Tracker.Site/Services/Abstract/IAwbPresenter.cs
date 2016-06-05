using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts.Awb;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.ViewModels.AirWaybill;

namespace Tracker.Services.Abstract
{
	public interface IAwbPresenter
	{
		ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId, long? senderUserId, string language);
		AwbAdminModel Get(long id);
		AirWaybillData GetData(long id);
		AirWaybillAggregate GetAggregate(long id);
		AwbSenderModel GetSenderAwbModel(long id);
		BrokerData GetBroker(long brokerId);
	}
}