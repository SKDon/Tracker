using System.Collections.Generic;
using Tracker.DataAccess.Contracts.Contracts;

namespace Tracker.Jobs.Helpers.Abstract
{
	internal interface ILocalizedDataHelper
	{
		IDictionary<string, string> Get(string language, EventDataForEntity eventData);
	}
}