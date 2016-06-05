using Tracker.DataAccess.Contracts.Contracts;

namespace Tracker.DataAccess.Contracts.Repositories.User
{
	public interface IClientFileRepository
	{
		FileHolder GetClientContract(long clientId);
		string GetClientContractFileName(long clientId);
		void SetClientContract(long clientId, string name, byte[] data);
	}
}
