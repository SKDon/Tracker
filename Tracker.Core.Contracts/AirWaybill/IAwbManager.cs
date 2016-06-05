using Tracker.DataAccess.Contracts.Contracts.Awb;

namespace Tracker.Core.Contracts.AirWaybill
{
	public interface IAwbManager
	{
		long Create(long? applicationId, AirWaybillEditData data, long creatorUserId);
		void Delete(long awbId);
	}
}