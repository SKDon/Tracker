using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface ISettingRepository
	{
		Setting AddOrReplace(Setting setting);
		Setting Get(SettingType type);
		T GetData<T>(SettingType type);
	}
}