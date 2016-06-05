using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface ITemplateRepository
	{
		EventTemplateData GetByEventType(EventType eventType);
		EmailTemplateLocalizationData GetLocalization(long templateId, string language);
		void SetForEvent(EventType eventType, string language, bool enableEmailSend,
			EmailTemplateLocalizationData localization);
	}
}