using System.Collections.Generic;
using Tracker.DataAccess.Contracts.Contracts;

namespace Tracker.Jobs.Helpers.Abstract
{
	public interface IClientExcelHelper
	{
		IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages);
	}
}