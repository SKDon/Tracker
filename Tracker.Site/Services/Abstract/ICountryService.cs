using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.ViewModels;

namespace Tracker.Services.Abstract
{
	public interface ICountryService
	{
		ListCollection<CountryData> List(int take, int skip, string language);
		CountryEditModel Get(long id);
		long Add(CountryEditModel model);
		void Edit(long id, CountryEditModel model);
	}
}