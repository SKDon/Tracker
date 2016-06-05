using Tracker.DataAccess.Contracts.Contracts.Awb;

namespace Tracker.Core.Contracts.AirWaybill
{
	public interface IAwbGtdHelper
    {
        void ProcessGtd(AirWaybillData data, string newGtd);
    }
}