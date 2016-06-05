using System.Collections.Generic;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Helpers.Abstract
{
	public interface ICommonFilesFacade
	{
		IReadOnlyDictionary<string, FileHolder[]> GetFiles(EventType type, EventDataForEntity eventData, string[] languages);
	}
}