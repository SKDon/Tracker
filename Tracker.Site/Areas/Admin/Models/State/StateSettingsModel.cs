using Tracker.ViewModels.EmailTemplate;

namespace Tracker.Areas.Admin.Models.State
{
	public sealed class StateSettingsModel
	{
		public EmailTemplateSettingsModel Availabilities { get; set; }
		public EmailTemplateSettingsModel Visibilities { get; set; }		
	}
}