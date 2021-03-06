using Tracker.DataAccess.Contracts.Contracts.User;

namespace Tracker.DataAccess.Contracts.Repositories.User
{
	public interface IBrokerRepository
	{
		BrokerData Get(long brokerId);
		BrokerData GetByUserId(long userId);
		BrokerData[] GetAll();
		long Update(long brokerId, string name, string login, string email);
		long Add(string name, string login, string email, string language);
	}
}