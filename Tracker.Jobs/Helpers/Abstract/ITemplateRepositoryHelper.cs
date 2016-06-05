using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Helpers.Abstract
{
	public interface ITemplateRepositoryHelper
	{
		long? GetTemplateId(EventType type);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}