using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.ViewModels;

namespace Tracker.Services.Abstract
{
	public interface ICityService
	{
		ListCollection<CityData> List(int take, int skip, string language);
		CityEditModel Get(long id);
		long Add(CityEditModel model);
		void Edit(long id, CityEditModel model);
	}
}