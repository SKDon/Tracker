using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Application.Abstract
{
	public interface IApplicationEventTemplates
	{
		long? GetTemplateId(EventType type, byte[] applicationEventData);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
	}
}