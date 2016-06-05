using System.Collections.ObjectModel;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories.Application
{
	public interface IApplicationFileRepository
	{
		ReadOnlyCollection<FileInfo> GetNames(long applicationId, ApplicationFileType type);
		ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> GetInfo(long[] applicationIds, ApplicationFileType type);
		FileHolder Get(long id);
		long Add(long applicationId, ApplicationFileType type, string fileName, byte[] bytes);
		void Delete(long id);
	}
}